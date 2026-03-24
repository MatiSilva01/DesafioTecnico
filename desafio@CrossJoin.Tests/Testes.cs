namespace desafio_CrossJoin.Tests;
using NUnit.Framework;

[TestFixture] 
public class Testes
{
    private Company company1;
    private Company company2;
    private Company company3;
    private Company company4;

    private Lead lead1;
    private Lead lead2;

    private Product product1;
    private Product product2;
    private Product product3;


    [SetUp]
    public void SetUp()
    {
        //construtor de company recebe string address, Country country, string stakeholder, string contact, int? nif = null
        company1 = new Company( "Rua da machada", Country.Portugal, "Mário Andrade", "mario.andrade@gmail.com", 12345678);
        company2 = new Company( "Napoles", Country.Italy, "Jane Albert", "jane.albert@gmail.com");
        company3 = new Company( "Madrid", Country.Spain, "Carlos Garcia", "carlos.garcia@gmail.com");
        company4 = new Company( "Leiria", Country.Portugal, "Maria Santos", "maria.santos@gmail.com");
        //construtor de lead recebe Company company, BusinessTypeEnum businessType
        lead1 = new Lead(company1, BusinessTypeEnum.FoodService);
        lead2 = new Lead(company2, BusinessTypeEnum.Retail);
        //Product(ProductTypeEnum productType, Product? dependentProduct = null)
        product1 = new Product(ProductTypeEnum.Electronics);
        product2 = new Product(ProductTypeEnum.Food);
        product3 = new Product(ProductTypeEnum.Electronics, product1); //produto dependente de outro do mesmo tipo, cumpre a regra

    }

    //COMPANY TESTS

    [Test]
    public void TestCompanyCreation() {
        //verificar se as empresas foram criadas corretamente
        Assert.That(company1.Country, Is.EqualTo(Country.Portugal));
        Assert.That(company1.Stakeholder, Is.EqualTo("Mário Andrade"));
        Assert.That(company1.Contact, Is.EqualTo("mario.andrade@gmail.com"));
        Assert.That(company1.NIF, Is.EqualTo(12345678));
        Assert.That(company1.Country, Is.EqualTo(Country.Portugal));
        //verificar se o ID incremental
        Assert.That(company1.ID, Is.LessThan(company2.ID));
        Assert.That(company2.ID, Is.LessThan(company3.ID));
        //verificar se o nif aparece null para company2 que nao é portuguesa
        Assert.That(company2.NIF, Is.EqualTo(null));
    }
    [Test]
    public void TestCompanyUpdate(){
        company1.UpdateCompanyAddress("Rua da machada numero 2");
        Assert.That(company1.Address, Is.EqualTo("Rua da machada numero 2"));
        company1.UpdateCompanyCountry(Country.Spain);
        Assert.That(company1.Country, Is.EqualTo(Country.Spain));
        company1.UpdateCompanyStatus(CompanyStatus.Active); //nao deve permitir atualizar para active sem ter uma proposta associada e sem essa proposta estar finalizada 
        Assert.That(company1.Status, Is.EqualTo(CompanyStatus.Draft));
        company1.UpdateCompanyStakeholder("Mário Andrade Silva");
        Assert.That(company1.Stakeholder, Is.EqualTo("Mário Andrade Silva"));
        company1.UpdateCompanyContact("mario.andrade.silva@gmail.com");
        Assert.That(company1.Contact, Is.EqualTo("mario.andrade.silva@gmail.com"));
        //update nif
        company2.UpdateCompanyNIF(12345678); //nao deve permitir atualizar o nif porque a empresa nao é portuguesa
        Assert.That(company2.NIF, Is.EqualTo(null));
        company4.UpdateCompanyNIF(99999999);
        Assert.That(company4.NIF, Is.EqualTo(99999999));//deve permitir atualizar nif porque é portuguesa
        //update country e nif
        company3.UpdateCompanyCountry(Country.Portugal);
        Assert.That(company3.Country, Is.EqualTo(Country.Spain));//nao atualiza porque tentou mudificar para Portugal sem nif
        company3.UpdateCompanyCountry(Country.Portugal, 11111111);
        Assert.That(company3.Country, Is.EqualTo(Country.Portugal));
    }

    //LEAD TESTS

    [Test]
    public void TestLeadCreation() {
        //verificar se as leads foram criadas corretamente
        Assert.That(lead1.Company, Is.EqualTo(company1));
        Assert.That(lead1.BusinessType, Is.EqualTo(BusinessTypeEnum.FoodService));
        Assert.That(lead1.Country, Is.EqualTo(company1.Country));
        Assert.That(lead1.Status, Is.EqualTo(StatusLeadEnum.Draft));
        Assert.That(lead1.LeadId, Is.LessThan(lead2.LeadId));
        Assert.That(lead2.Company, Is.EqualTo(company2));
        Assert.That(lead2.BusinessType, Is.EqualTo(BusinessTypeEnum.Retail));
    }
    [Test]
    public void TestLeadUpdate() {
        lead1.UpdateLeadBusinessType(BusinessTypeEnum.Retail);
        Assert.That(lead1.BusinessType, Is.EqualTo(BusinessTypeEnum.Retail));
        lead1.UpdateLeadStatus(StatusLeadEnum.Accepted);
        Assert.That(lead1.Status, Is.EqualTo(StatusLeadEnum.Draft));//sem a criacao de uma proposta, o status do lead nao deve mudar para active
        lead1.UpdateLeadStatus(StatusLeadEnum.Rejected);
        Assert.That(lead1.Status, Is.EqualTo(StatusLeadEnum.Rejected));//mas pode ser mudado para outro estado 
    }

    //Product tests
    [Test]
    public void TestProductCreation() {
        Assert.That(product1.ProductType, Is.EqualTo(ProductTypeEnum.Electronics));
        Assert.That(product1.DependentProduct, Is.EqualTo(null));
        Assert.That(product3.ProductType, Is.EqualTo(ProductTypeEnum.Electronics));
        Assert.That(product3.DependentProduct, Is.EqualTo(product1)); //produto dependente de outro do mesmo tipo
        //Nao deve permitir criar um produto dependente de outro de tipo diferente
        Assert.Throws<InvalidOperationException>(() => new Product(ProductTypeEnum.Food, product1));
        //verificar lista de produtos filhos
        Assert.That(product1.DependentProducts, Does.Contain(product3)); //produto1 tem como produto dependente o produto3
        Assert.That(product2.DependentProducts, Is.Empty); //produto2 nao tem produtos dependentes
    }
    [Test]
    public void TestProductUpdateType() {
        //atualizar tipo de produtos que tem dependentes - não deve ser possivel porque tem filhos
        Assert.Throws<InvalidOperationException>(() => product1.UpdateProductType(ProductTypeEnum.Food));
         //atualizar tipo de produto que nao tem dependetes (nao é pai de outros) e nao é filho de nenhum produto (nao é dependente de nenhum produto) - deve ser possivel atualizar para qualquer tipo
        product2.UpdateProductType(ProductTypeEnum.Clothes);
        Assert.That(product2.ProductType, Is.EqualTo(ProductTypeEnum.Clothes));
        //atualizar tipo de produto que não tem dependetes (não e pai de outtros) mas é filho de algum (é dependente)
        //não deve ser possivel se tiver tipo diferente do produto de quem e filho
        Assert.Throws<InvalidOperationException>(() => product3.UpdateProductType(ProductTypeEnum.Food));
        //deve ser possivel atualizar se for do mesmo tipo do produto de quem é filho
        product3.UpdateProductType(ProductTypeEnum.Electronics); //fica na mesma basicamente - nao da erro
        Assert.That(product3.ProductType, Is.EqualTo(ProductTypeEnum.Electronics));
    }

    [Test ]
    public void TestProductUpdateDependentProduct() {
        //se puser que e filho de um produto com tipo difetente do seu nao deve permitir
        //product 2 e do tipo clothes e 1 é do tipo eletronic
        Assert.Throws<InvalidOperationException>(() => product2.UpdateDependentProduct(product1));
        //se puser que é filho de um produto com o mesmo tipo deve permitir
        Product product4 = new Product(ProductTypeEnum.Clothes);
        Product product5 = new Product(ProductTypeEnum.Clothes);
        product4.UpdateDependentProduct(product5);
        Assert.That(product4.DependentProduct, Is.EqualTo(product5));
        Assert.That(product5.DependentProducts, Does.Contain(product4));//a lista de filhos do pai 5 deve ter o filho 4
        //ja tem um pai mas queremos definir outro pai (com o mesmo tipo, deve permitir)
        Product product6 = new Product(ProductTypeEnum.Clothes);
        product4.UpdateDependentProduct(product6);
        Assert.That(product4.DependentProduct, Is.EqualTo(product6));
        Assert.That(product5.DependentProducts, Does.Not.Contain(product4)); //o produto 4 ja nao é filho do produto 5 pelo que nao deve estar na lista de filhos do 5
        Assert.That(product6.DependentProducts, Does.Contain(product4)); //o produto 4 agora é filho do produto 6 pelo que deve estar na lista de filhos do 6
        //ja tem um pai mas queremos definir outro pai (com tipo diferente, nao deve permitir e deve manter o mesmo pai que ja tinha)
        Assert.Throws<InvalidOperationException>(() => product4.UpdateDependentProduct(product1));
        Assert.That(product4.DependentProduct, Is.EqualTo(product6));
    }

}