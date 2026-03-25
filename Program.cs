// See https://aka.ms/new-console-template for more information
class Program
{

    static void Main()
    {
        Console.WriteLine("=== Demonstração challenge ===\n");

        // Configurar regras de validação
        RuleManager ruleManager = new RuleManager();
        ruleManager.SetRequired("Company", "NIF", RuleManager.CompanyIsInPortugal, true);

        // Criar empresas
        //construtor company: string address, Country country, string stakeholder, string contact, int? nif = null
        var company1 = new Company( "Rua da machada", Country.Portugal, "Mário Andrade", "mario.andrade@gmail.com", 12345678);
        var company2 = new Company( "Napoles", Country.Italy, "Jane Albert", "jane.albert@gmail.com");
        var company3 = new Company( "Leiria", Country.Portugal, "Maria Santos", "maria.santos@gmail.com");
        var company4 = new Company( "Madrid", Country.Spain, "Carlos Garcia", "carlos.garcia@gmail.com");
        Console.WriteLine("\nEmpresas criadas:");
        Console.WriteLine($"Company 1: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF}");
        Console.WriteLine($"Company 2: {company2.Stakeholder}, Country: {company2.Country}, Status: {company2.Status}, NIF: {company2.NIF} ");
        Console.WriteLine($"Company 3: {company3.Stakeholder}, Country: {company3.Country}, Status: {company3.Status}, NIF: {company3.NIF} ");
        Console.WriteLine("\nValidar NIF das empresas:");
        
// Validar company1 (Portugal com NIF) - deve passar
var errors1 = ruleManager.ValidateRules(company1);
Console.WriteLine($"Company 1 (Portugal com NIF): {(errors1.Count == 0 ? "Portuguesa e com NIF: Válida" : $"Invalida: {string.Join(", ", errors1)}")}");

// Validar company2 (Italy sem NIF) - deve passar (não é Portugal)
var errors2 = ruleManager.ValidateRules(company2);
Console.WriteLine($"Company 2 (Italy): {(errors2.Count == 0 ? "Não tem NIF mas não é Portuguesa: Válida" : $"Erros: {string.Join(", ", errors2)}")}");
// Validar company4 (Portugal SEM NIF) - deve falhar
var errors4 = ruleManager.ValidateRules(company4);
Console.WriteLine($"Company 3 (Portugal SEM NIF): {(errors4.Count == 0 ? "✓ Válida" : $"Invalida: portuguesa e sem NIF: - {errors4[0]}")}");

Console.WriteLine("\nAtualizar informações da Company 1:");
Console.WriteLine($"Antes da atualização: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF} ");
Console.WriteLine("Tentar atualizar status da Company 1 para Active sem proposta associada:");
try {//nao deve permitir atualizar para active sem ter uma proposta associada e sem finalizar a proposta
    company1.UpdateCompanyStatus(CompanyStatus.Active); //nao deve permitir atualizar para active sem ter uma proposta associada e sem finalizar a proposta
}
catch (Exception ex){
    Console.WriteLine($"Erro: {ex.Message}");
}
Console.WriteLine("Tentar atualizar o NIF, address e country da Company 1:");
company1.UpdateCompanyNIF(87654321);
company1.UpdateCompanyAddress("Rua nova");
company1.UpdateCompanyCountry(Country.Spain);
Console.WriteLine("\nCompany1 depois de atualizar as informacoes:");
Console.WriteLine($"Company 1: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF} ");

Console.WriteLine("\n=== Leads ===\n");
var lead1 = new Lead(company3, BusinessTypeEnum.Industry);
var lead2 = new Lead(company2, BusinessTypeEnum.Retail);
var lead3 = new Lead(company1, BusinessTypeEnum.FoodService);
Console.WriteLine("\nLeads criados:");
Console.WriteLine($"Lead 1: ID={lead1.LeadId}, Company ID={lead1.Company.ID}, Country={lead1.Country}, Business Type={lead1.BusinessType}, Status={lead1.Status}");
Console.WriteLine($"Lead 2: ID={lead2.LeadId}, Company ID={lead2.Company.ID}, Country={lead2.Country}, Business Type={lead2.BusinessType}, Status={lead2.Status}");
lead1.UpdateLeadBusinessType(BusinessTypeEnum.FoodService);
Console.WriteLine("\nupdate business type do Lead 1 para Foodservice:");
Console.WriteLine($"Lead 1: ID={lead1.LeadId}, Company ID={lead1.Company.ID}, Country={lead1.Country}, Business Type={lead1.BusinessType}, Status={lead1.Status}");

        Console.WriteLine("\n-- Converter Leads em Propostas:--");
        var proposal1 = lead1.leadToProposal();
        var proposal2 = lead2.leadToProposal();
        //var proposal3 = lead2.leadToProposal(); //nao deve aceitar pq ja criou uma lead
        Console.WriteLine("\nPropostas criadas a partir dos leads:");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}");
        try {
            Console.WriteLine("\nTentar criar uma nova proposta a partir do Lead 2 (que já tem uma proposta associada):");
            var proposal3 = lead2.leadToProposal(); //nao deve aceitar pq ja criou uma lead
            Console.WriteLine($"Proposal 3: ID={proposal3.ProposalID}, Lead ID={proposal3.Lead.LeadId}, Company ID={proposal3.Lead.Company.ID}, Products Count={proposal3.Products.Count}, Status={proposal3.Status}");
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
    

        // Criar produtos
        Console.WriteLine("\n===Criar Produtos:===");
        var product1 = new Product(ProductTypeEnum.Electronics, null);
        var product2 = new Product(ProductTypeEnum.Furniture, null);
        var product4 = new Product(ProductTypeEnum.Food, null);
        var product5 = new Product(ProductTypeEnum.Food, product4); //deve aceitar porque o tipo do produto 1 é igual ao tipo do produto 5

        Console.WriteLine("\nProdutos criados:");
        Console.WriteLine($"Product 1: ID={product1.ProductID}, Type={product1.ProductType}, Dependent on Product ID={product1.DependentProduct?.ProductID}");
        Console.WriteLine($"Product 2: ID={product2.ProductID}, Type={product2.ProductType}, Dependent on Product ID={product2.DependentProduct?.ProductID}");
        try{
            Console.WriteLine("Tentar criar um produto do tipo Electronics que depende do produto 2 que é do tipo Furniture:");
            var product3 = new Product(ProductTypeEnum.Electronics, product2); //nao deve aceitar porque o tipo do produto 2 é diferente do tipo do produto 3
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine($"Product 4: ID={product4.ProductID}, Type={product4.ProductType}, Dependent on Product ID={product4.DependentProduct?.ProductID}");
        Console.WriteLine($"Product 5: ID={product5.ProductID}, Type={product5.ProductType}, Dependent on Product ID={product5.DependentProduct?.ProductID}");
        Console.WriteLine("\nTentartualizar o produto 2 para depender do produto 1:");
        try{ 
            product2.UpdateDependentProduct(product1);
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine($"Product 2: ID={product2.ProductID}, Type={product2.ProductType}, Dependent on Product ID={product2.DependentProduct?.ProductID}");
        
        // Adicionar produtos às propostas
        Console.WriteLine("\n== Adicionar Produtos às Propostas:==");
        Console.WriteLine("Adicionar produtos sem dependências à Proposal 1:");
        // Ordem correta: primeiro produtos sem dependências, depois os dependentes
        proposal1.AddProduct(product1); // Electronics, sem dependência
        Console.WriteLine($"Produto 1  tipo = {product1.ProductType} adicionado à Proposal 1");
        proposal1.AddProduct(product2); // Furniture, depende de product1 (Electronics) - mas tipos diferentes!
        Console.WriteLine($"Produto 2  tipo = {product2.ProductType} adicionado à Proposal 1");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Products Count={proposal1.Products.Count}");
         Console.WriteLine("\nPropostas após adição de produtos:");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}");
        Console.WriteLine("\nTentar adicionar o produto (product5) com dependência a outro prooduto (product4) à Proposal 1 sem que o produto pai (4) esteja na proposta:");
        try{
            proposal1.AddProduct(product5); //dnao eve aceitar porque o tipo do produto 5 é food e ele deende do 4 que ainda nao esta na proposta
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine("\nAdicionar o produto 4 (food) à Proposal 1 (produto pai) e posteriormete tentar adicionar o produto 5 (food) com dependência a ele:");
        proposal1.AddProduct(product4); //deve aceitar porque o produto 4 é do tipo food e nao tem dependências
        Console.WriteLine($"Produto 4 tipo = {product4.ProductType} adicionado à Proposal 1");
        proposal1.AddProduct(product5); //deve aceitar porque o produto 5 é do tipo food e ele deende do 4 que ja esta na proposta
        Console.WriteLine($"Produto 5 tipo = {product5.ProductType} adicionado à Proposal 1");
        Console.WriteLine("\nPropostas após adição de produtos:");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}");

        Console.WriteLine("\nTentar remover o produto 4 (produto pai) da Proposal 1 sem remover primeiro o produto 5 (produto dependente):");
        try{
            proposal1.RemoveProduct(product4); //nao deve aceitar porque o produto 4 é pai do produto 5
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine("\nRemover primeiro o produto 5 (produto dependente) e depois o produto 4 (produto pai) da Proposal 1:");
        proposal1.RemoveProduct(product5); //deve aceitar porque o produto 5 é dependente do produto 4
        Console.WriteLine($"Produto 5 removido da Proposal 1");
        proposal1.RemoveProduct(product4); //deve aceitar porque o produto 4 nao tem mais dependentes na proposta
        Console.WriteLine($"Produto 4 removido da Proposal 1");
        Console.WriteLine("\nPropostas após remoção de produtos:");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        
        Console.WriteLine("\n-- Update Detalhes das Propostas:--");


        Console.WriteLine($"\nProposal 1 production cost: {proposal1.ProductionCost}");
        Console.WriteLine($"\nProposal 1 monthly produced products: {proposal1.MonthlyProducedProducts}");
        Console.WriteLine($"\nProposal 1 expected profit: {proposal1.ExpectedMonthlyProfit}");
        Console.WriteLine("\nupdate production cost, monthly produced products e expected monthly profit da Proposal 1:");
        proposal1.UpdateProposalProductionCost(5000.0);
       // Console.WriteLine($"\nProposal 1 cost: {proposal1.ProductionCost}");
        proposal1.UpdateProposalMonthlyProducedProducts(100);
        proposal1.UpdateProposalExpectedMonthlyProfit(2000.0);
        //Console.WriteLine($"\nProposal 1 monthly produced products: {proposal1.MonthlyProducedProducts}");
        //Console.WriteLine($"\nProposal 1 expected profit: {proposal1.ExpectedMonthlyProfit}");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}, Production Cost={proposal1.ProductionCost}, Monthly Produced Products={proposal1.MonthlyProducedProducts}, Expected Monthly Profit={proposal1.ExpectedMonthlyProfit}");
        Console.WriteLine("\nProposal 2 sem atualizar detalhes:");
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}, Production Cost={proposal2.ProductionCost}, Monthly Produced Products={proposal2.MonthlyProducedProducts}, Expected Monthly Profit={proposal2.ExpectedMonthlyProfit}");
        Console.WriteLine($"Company 2: {company2.Stakeholder}, Country: {company2.Country}, Status: {company2.Status}, NIF: {company2.NIF} ");

        // Finalizar propostas
        Console.WriteLine("\n=== Finalize Proposals:==");
        Console.WriteLine("Finalizar Proposal 1:");
        proposal1.FinalizeProposal();
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        Console.WriteLine("Tentar finalizar a Proposal 2 sem atualizar os detalhes necessários:");
        try{
            proposal2.FinalizeProposal(); //nao deve aceitar porque os detalhes da proposta nao foram atualizados
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine("\nAdicionar produtos para proposal2 e tentar finalizar novamente:");
        proposal2.AddProduct(product1); //adicionar um produto para a proposta ter produtos e atualizar os detalhes
        try{
            proposal2.FinalizeProposal(); //nao deve aceitar porque os detalhes da proposta nao foram atualizados
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine("\nAtualizar detalhes da Proposal 2 e tentar finalizar novamente:");
        proposal2.UpdateProposalProductionCost(3000.0);
        proposal2.UpdateProposalMonthlyProducedProducts(50);
        proposal2.UpdateProposalExpectedMonthlyProfit(1000.0);
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}, Production Cost={proposal2.ProductionCost}, Monthly Produced Products={proposal2.MonthlyProducedProducts}, Expected Monthly Profit={proposal2.ExpectedMonthlyProfit}");
        Console.WriteLine("Finalizar Proposal 2:");
        try{
            proposal2.FinalizeProposal(); //deve aceitar porque os detalhes da proposta foram atualizados
            Console.WriteLine("Proposal 2 finalizada com sucesso.");
        }
        catch (Exception ex){
            Console.WriteLine($"Erro: {ex.Message}");
        }
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}, Production Cost={proposal2.ProductionCost}, Monthly Produced Products={proposal2.MonthlyProducedProducts}, Expected Monthly Profit={proposal2.ExpectedMonthlyProfit}");

        
    
    }
}
