# Desafio Mundipagg

### Introdução 


Desafio consiste em criar uma Api de produtos escrita em Asp.net Web Api 2.0. É necessário uma Api que faça a seguinte lista abaixo:
  
  1. Cadastrar um novo produto  
  2. Editar um produto cadastrado
  3. Excluir um produto
  4. Obter detalhes de um produto
  5. Listar todos os produtos existentes de forma paginada
  
  
### Arquitetura

A aplicação foi escrita com modelo de domínio DDD(Domain Driven Design), com abstração mais simples em sua composição de camadas. A estrutura do projeto consiste em:

   > Camada de API no qual expõe os endopoints necessários
   
   > Camada de Dto para expor os contratos
   
   > Camada de Aplicação para blindar o modelo de domínio e orquestrar as funcionalidades
   
   > Camada de Domínio na qual compõe o modelo de negócio, os contratos de repositório e a regra de negócio do sistema
   
   > Camada de acesso à dados que acesso um repositório de dados Mongo(Cluster gratuito na AWS)
   
   > Camada de Infraestruta com componentes necessários para funcionamento de diversas camadas do sistema.
   
   > Camada de IOC para injeção de dependência e acesso à todos recursos de camadas do sistema
   
   > Camada de teste unitários e testes de Api

#### Possíveis arquiteturas
Pelo fato do sistema ser de complexidade baixa, não foi necessário pensar em outros padrões com CQRS, Eventsourcing etc. 
   
A aplicação foi escrita em REST respeitando alguma diretrizes de status HTTP de resposta no sistema:
 
 ```C#

 [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductApplication productApplication;
        public ProductController(IProductApplication productApplication) =>
            this.productApplication = productApplication;

        [HttpGet("pagin/{pagin}/itemsPerPagin/{itemsPerPagin}")]
        public IEnumerable<ProductDto> Get(int pagin, int itemsPerPagin)
        {
            return productApplication.All(itemsPerPagin, pagin);
        }

        [HttpGet("{id}")]
        public ProductDto Get(Guid id)
        {
            return productApplication.Detail(id);
        }

        [HttpPost]
        public StatusCodeResult Post([FromBody]AddProductDto productDto)
        {
            productApplication.Add(productDto);
            return new StatusCodeResult(201);
        }

        [HttpPut("{id}")]
        public StatusCodeResult Put(Guid id, [FromBody]EditProductDto productDto)
        {
            productApplication.Edit(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(Guid id)
        {
            productApplication.Remove(id);
            return NoContent();
        }
    }

````


 ### Recursos utilizados
 
    * MongoDB.Driver
    * AutoMapper
    * FluentValidation
    * Projetos de teste: Moq, FluentAssertions e XUnit
    
  
 ### Melhorias necessárias
 
 Algumas melhorias necessárias:
 
Teste :  Será necessário uma melhoria no entendimento no ciclo de vida de testes para criação dos mapeamentos do Automapper, exemplo abaixo, foi uma solução de contorno para que não fosse gerado erro  na classe MapperContext:
    
```C#

 public class MapperContext
    {
        private static IMapper mapper;

        public static IMapper GetMapper()
        {
            Mapper.Reset();

            try
            {
                Mapper.Initialize(x => x.AddProfile(new MappingProfile()));
            }
            catch 
            {
            }
           
            mapper = Mapper.Configuration.CreateMapper();
            return mapper;
        }
    }
    
````
Documentação : Swagger para documentação exposição dos endpoints
    
Melhoria no modelo de domínio, no mínimo a criação de um atributo para data de criação do produto, talvez uma ramificação de Skus

```C#

 public class Product : AggregateRoot
    {
        private Product() : base() { }

        public Product(string name, string code, string departamentName)
        {
            Name = name;
            Code = code;
            Departament = new Departament(departamentName);
        }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public Departament Departament { get; private set; }

    }

````

### Docker & DockerCompose

Não foi implementado nada em Docker, pelo fato do uso da ferramenta de para uso de containeres ser um pouco mais complexo em sistemas Windows, pelo fato de virtualização etc. Foi feita uma tentativa de instalação numa máquina virtual Linux Mint 18, mesmo assim foram encontrados alguns problemas. 

Pelo fato do tempo ter sido um pouco curto, foi dado foco na entrega mais básica do funcionamento do sistema e seus testes.


### Exemplos de algumas camadas:

Teste unitários e de API: 

```C#
// Testes unitários
[Fact]
        public void GetProduct()
        {
            var produto = ProductBuilder
                            .Create
                            .WithCode("testCode")
                            .WithName("testName")
                            .WithDepartment("testDepartament")
                            .Instance();

            repositorioMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(() => produto);
            Guid guid = Guid.NewGuid();

            mapper.Setup(x => x.Map<ProductDto>(It.IsAny<Product>()))
                .Returns(() => 
                            new ProductDto
                            {
                                Id = guid,
                                Code = "testCode",
                                Name = "testName",
                                Departament =  new DepartamentDto { Name = "testDepartament" }
                            });

            var productDto = productApplication.Detail(guid);

            productDto.Should().NotBeNull();
            productDto.Should().BeOfType<ProductDto>();
            productDto.Name.Should().Be(produto.Name);
            productDto.Code.Should().Be(produto.Code);
            productDto.Id.Should().Be(guid);
            productDto.Departament.Name.Should().Be(produto.Departament.Name);

        }
        
 // Teste de Api
 
 [Fact]
        public void UpdateProduct()
        {
            var produtoBuilder = ProductBuilder.Create
                                    .WithCode("testCode")
                                    .WithName("testName")
                                    .WithDepartment("testDepartament");

            var product = produtoBuilder.Instance();
            repositorio.Add(product);

            produtoBuilder
                .WithId(product.Id)
                .WithCode("testCode new")
                .WithName("testName new")
                .WithDepartment("testDepartament new");

            var statusCodeResult = productController.Put(product.Id, produtoBuilder.ToEditProductDto());
            statusCodeResult.StatusCode.Should().Be(204);

            var newProduct = repositorio.Get(product.Id);
            newProduct.Should().NotBeNull();
            newProduct.Name.Should().Be("testName new");
            newProduct.Code.Should().Be("testCode new");

        }

````


Repositório :

```C#

 public class ProductMongoRepository : BaseRepository, IProductMongoRepository
    {
        public ProductMongoRepository() 
        {

        }
        public void Add(Domain.Products.Product entity)
        {
            this.db.GetCollection<Product>("Product").InsertOne(entity);
        }

        public void Edit(Domain.Products.Product entity)
        {
            this.db.GetCollection<Product>("Product")
                    .ReplaceOne(x => x.Id == entity.Id, entity);
        }

        public Product Get(Guid id)
        {
            return this.db.GetCollection<Product>("Product")
                    .Find(x => x.Id == id)
                    .FirstOrDefault();
        }

        public IEnumerable<Product> GetAll(int pagin, int paginRows)
        {
            return this.db.GetCollection<Product>("Product")
                            .AsQueryable()
                            .Page(paginRows, pagin)
                            .ToList();
         }

        public void Remove(Guid id)
        {
            this.db.GetCollection<Product>("Product").DeleteOne(x => x.Id == id);
        }
    }

````

Criação de controle de erros no Middleware do sistema
```C#

public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exc = new ExceptionError();
            exc.Error = context.Exception.GetBaseException().Message;
            
            context.HttpContext.Response.StatusCode = 400;

            if (context.Exception is DomainException)
            {
                var ex = context.Exception as DomainException;
                context.Exception = ex;
                exc.Message = "Houve um erro de negócio";
            }
            else
            {
               
#if !DEBUG              
                string stack = null;
#else
                string stack = context.Exception.StackTrace;
#endif

                exc.Message = "Erro inesperado no sistema";
                exc.Detail = stack;

                context.HttpContext.Response.StatusCode = 500;

            }

            context.Result = new JsonResult(exc);

            base.OnException(context);
        }

````

