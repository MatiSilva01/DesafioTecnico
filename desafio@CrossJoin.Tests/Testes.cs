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
    private Product product4;
    private Product product5;
    private Product product6;

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
        product4 = new Product(ProductTypeEnum.Food); 
        product5 = new Product(ProductTypeEnum.Food, product4); //produto dependente de outro do mesmo tipo, cumpre a regra
        product6 = new Product(ProductTypeEnum.Clothes);
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
        Assert.That(lead1.Status, Is.EqualTo(StatusLeadEnum.Draft));//sem a criacao de uma proposta, o status do lead e draft
    }

    //Product tests
    [Test]
    public void TestProductCreation() {
        Assert.That(product1.ProductID, Is.LessThan(product2.ProductID));
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

    //PROPOSAL TESTS
    [Test]
    public void TestProposalCreation() {
        //Construtor de Proposal recebe Lead
        //propostas apenas criadas aqui porque se em cima alterava o estado da lead devido a ja haver umaproposal e os testes nao batiam certo
        var proposal1 = lead1.leadToProposal();
        var proposal2 = new Proposal(lead2);
        Assert.That(proposal1.ProposalID, Is.LessThan(proposal2.ProposalID));
        //verificar se a proposta foi criada corretamente com os dados herdados da lead quando criada com o leadToProposal
        Assert.That(proposal1.Company, Is.EqualTo(company1));
        Assert.That(proposal1.Country, Is.EqualTo(company1.Country));
        Assert.That(proposal1.Status, Is.EqualTo(ProposalStatusEnum.Draft));
        //verificar se a proposta foi criada corretamente com os dados herdados da lead quando criada com o construtor de Proposal
        Assert.That(proposal2.Company, Is.EqualTo(company2));
        Assert.That(proposal2.Country, Is.EqualTo(company2.Country));
        Assert.That(proposal2.Status, Is.EqualTo(ProposalStatusEnum.Draft));
        //tentar criar propostas a partir da mesma lead com o LeadToProposal
        //tentar criar outra proposta a partir da mesma lead1 atraves de lead1.leadToProposal() - nao deve permitir porque o status do lead1 ja é accepted
        Assert.Throws<InvalidOperationException>(() => lead1.leadToProposal());
        //tentar criar outra proposta a partir da mesma lead1 atraves do construtor de Proposal - nao deve permitir porque o status do lead1 ja é accepted
        Assert.Throws<InvalidOperationException>(() => new Proposal(lead1));
        //tentar criar propostas a partir da mesma lead com o construtor de Proposal
        //tentar criar outra proposta a partir da mesma lead2 atraves de lead2.leadToProposal() - nao deve permitir porque o status do lead1 ja é accepted
        Assert.Throws<InvalidOperationException>(() => lead2.leadToProposal());
        //tentar criar outra proposta a partir da mesma lead1 atraves do construtor de Proposal - nao deve permitir porque o status do lead1 ja é accepted
        Assert.Throws<InvalidOperationException>(() => new Proposal(lead2));
    }
    [Test]
    public void TestProposalUpdate() {
        var proposal1 = lead1.leadToProposal();
        //antes de fazer update não tem valores iniciais e o estado e draft
        Assert.That(proposal1.ProductionCost, Is.EqualTo(0));
        Assert.That(proposal1.MonthlyProducedProducts, Is.EqualTo(0));
        Assert.That(proposal1.ExpectedMonthlyProfit, Is.EqualTo(0));
        Assert.That(proposal1.Status, Is.EqualTo(ProposalStatusEnum.Draft));
        //atualizar os dados da proposta
        proposal1.UpdateProposalProductionCost(1000);
        Assert.That(proposal1.ProductionCost, Is.EqualTo(1000));
        proposal1.UpdateProposalMonthlyProducedProducts(500);
        Assert.That(proposal1.MonthlyProducedProducts, Is.EqualTo(500));
        proposal1.UpdateProposalExpectedMonthlyProfit(2000);
        Assert.That(proposal1.ExpectedMonthlyProfit, Is.EqualTo(2000));

        //tentar definir valores invalidos
        Assert.Throws<InvalidOperationException>(() => proposal1.UpdateProposalProductionCost(0));
        Assert.Throws<InvalidOperationException>(() => proposal1.UpdateProposalMonthlyProducedProducts(-10));
        Assert.Throws<InvalidOperationException>(() => proposal1.UpdateProposalExpectedMonthlyProfit(-5));
        //tenatar definir o estado da proposta para approved manualmente - nao deve permitir
        Assert.Throws<InvalidOperationException>(() => proposal1.UpdateProposalStatus(ProposalStatusEnum.Approved));
        //verificar que status continua draft
        Assert.That(proposal1.Status, Is.EqualTo(ProposalStatusEnum.Draft));
    }
    //test add product to proposal
    [Test]
    public void TestAddProductToProposal() {
        var proposal1 = lead1.leadToProposal();
        //adicionar produto com dependencias sem ter o produto de que é dependente na lista de produtos da proposta - nao deve permitir
        //proposal1.AddProduct(product5);
        Assert.Throws<InvalidOperationException>(() => proposal1.AddProduct(product5)); //nao deve permitir adicionar o produto5 porque a sua dependencia (produto4) nao esta na lista de produtos da proposta
        Assert.That(proposal1.Products, Does.Not.Contain(product5));
        //adicionar produto com dependencias tendo o produto de que é dependente na lista de produtos da proposta - deve permitir
        proposal1.AddProduct(product4);
        proposal1.AddProduct(product5);
        Assert.That(proposal1.Products, Does.Contain(product5));
        Assert.That(proposal1.Products, Does.Contain(product4));
        //adicionar produto sem dependencias
        proposal1.AddProduct(product6);
        Assert.That(proposal1.Products, Does.Contain(product6));
        //remover produto da lista
        //remover produto sem dependencias - deve permitir
        proposal1.RemoveProduct(product6);
        Assert.That(proposal1.Products, Does.Not.Contain(product6));
        //remover produto que tem dependentes na lista - não deve permitir, deve informar que deve primeiro remover os produtos dependentes
        Assert.Throws<InvalidOperationException>(() => proposal1.RemoveProduct(product4));//porque o produto 5  depende do produto 4
        Assert.That(proposal1.Products, Does.Contain(product4));
        //remover produto que é dependente de outro - deve permitir
        proposal1.RemoveProduct(product5);
        Assert.That(proposal1.Products, Does.Not.Contain(product5));
        //como ja removemos produtos dependentes do produto 4, agora deve ser possivel remover o produto 4
        proposal1.RemoveProduct(product4);
        Assert.That(proposal1.Products, Does.Not.Contain(product4));
    }
    [Test]
    public void TestFinalizeProposal() {
        var proposal1 = lead1.leadToProposal();
        //o status da company associada a proposal e a lead deve ser draft
        Assert.That(proposal1.Company.Status, Is.EqualTo(CompanyStatus.Draft));
        //o estado da lead inicial deve ser accepted
        Assert.That(lead1.Status, Is.EqualTo(StatusLeadEnum.Accepted));
        //finalizar proposta sem produtos - nao deve permitir porque a proposta tem de ter pelo menos um produto
        Assert.Throws<InvalidOperationException>(() => proposal1.FinalizeProposal());
        //o status deve continuar draft
        Assert.That(proposal1.Status, Is.EqualTo(ProposalStatusEnum.Draft));
        //adcionar produto e tentar finalizar - nao deve permitir porque deve ter productionCost, monthlyProducedProducts e expectedMonthlyProfit superiores a 0
        proposal1.AddProduct(product6);
        Assert.Throws<InvalidOperationException>(() => proposal1.FinalizeProposal());
        //o estado deve continuar draft
        Assert.That(proposal1.Status, Is.EqualTo(ProposalStatusEnum.Draft));
        //adicionar produto e definir restantes dados 
        proposal1.AddProduct(product6);
        proposal1.UpdateProposalProductionCost(1000);
        proposal1.UpdateProposalMonthlyProducedProducts(500);
        proposal1.UpdateProposalExpectedMonthlyProfit(2000);
        proposal1.FinalizeProposal();
        Assert.That(proposal1.Status, Is.EqualTo(ProposalStatusEnum.Approved));
        //o status da company associada a proposal e a lead deve ter passado a active
        Assert.That(proposal1.Company.Status, Is.EqualTo(CompanyStatus.Active));}
    
}