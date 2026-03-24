// See https://aka.ms/new-console-template for more information
class Program
{

    static void Main()
    {
        Console.WriteLine("=== Sistema de Gestão de Empresas, Leads, Propostas e Produtos ===\n");

        // Configurar regras de validação
        //SetupValidationRules();
        RuleManager ruleManager = new RuleManager();
        //se for portugues tem de ter nif
        //Rule rule = new Rule("Company", "NIF", RuleManager.CompanyIsInPortugal, true);
        //ruleManager.AddRule(rule);

        // Criar empresas
        //construtor company: string address, Country country, string stakeholder, string contact, int? nif = null
        var company1 = new Company( "Rua da machada", Country.Portugal, "Mário Andrade", "mario.andrade@gmail.com", 12345678);
        var company2 = new Company( "Napoles", Country.Italy, "Jane Albert", "jane.albert@gmail.com");
        var company3 = new Company( "Madrid", Country.Spain, "Carlos Garcia", "carlos.garcia@gmail.com");
        var company4 = new Company( "Leiria", Country.Portugal, "Maria Santos", "maria.santos@gmail.com");
        Console.WriteLine("\nEmpresas criadas:");
        Console.WriteLine($"Company 1: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF}");
        Console.WriteLine($"Company 2: {company2.Stakeholder}, Country: {company2.Country}, Status: {company2.Status}, NIF: {company2.NIF} ");
        Console.WriteLine($"Company 4: {company4.Stakeholder}, Country: {company4.Country}, Status: {company4.Status}, NIF: {company4.NIF} ");
        company1.UpdateCompanyStatus(CompanyStatus.Active); //nao deve permitir atualizar para active sem ter uma proposta associada e sem finalizar a proposta
        Console.WriteLine("\nTentativa de atualizar status da Company 1 para Active sem proposta associada:");
        Console.WriteLine($"Company 1: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF} ");
        Rule regraNIFObrigatorio = new Rule("Company", "NIF", RuleManager.CompanyIsInPortugal, true);
        ruleManager.AddRule(regraNIFObrigatorio);
        Console.WriteLine("Adicionada regra de NIF obrigatório para empresas em Portugal\n");
        var validationErrors = ruleManager.Validate(company1);
        var validationErrors2 = ruleManager.Validate(company2);
        var validationErrors4 = ruleManager.Validate(company4);
        Console.WriteLine("Validar nif da company1 - deve passar\n");
        if (validationErrors.Count == 0){
            Console.WriteLine("company 1 tem nif valido\n");
        }else{
            foreach (var error in validationErrors){
                Console.WriteLine($"é necessário adicionar o nif para empresas portuguesas\n");
            }
        }
        Console.WriteLine("Validar nif company2 - não é Portuguesa não necessita nif\n");
        if (validationErrors2.Count == 0){
            Console.WriteLine("Validada\n");
        }else{
            foreach (var error in validationErrors2){
                Console.WriteLine($"erro de validacao: {error}\n");
            }
        }
        Console.WriteLine("Validar nif da company4 - deve falhar por falta de nif\n");
        if (validationErrors4.Count == 0){
            Console.WriteLine("company 4 tem nif valido\n");
        }else{
            foreach (var error in validationErrors4){
                Console.WriteLine($"é necessário adicionar o nif para empresas portuguesas\n");
            }
        }
        Console.WriteLine($"Company 1: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF} ");
        company1.UpdateCompanyNIF(87654321);
        company1.UpdateCompanyAddress("Rua nova");
        company1.UpdateCompanyCountry(Country.Spain);
        company1.UpdateCompanyStakeholder("Maria Silvaa");
        company1.UpdateCompanyContact("mariagmail.com");
        company1.UpdateCompanyStatus(CompanyStatus.Active);
        Console.WriteLine("\nCompany1 depois de atualizar as informacoes:");
        Console.WriteLine($"Company 1: {company1.Stakeholder}, Country: {company1.Country}, Status: {company1.Status}, NIF: {company1.NIF} ");
        var validationErrors1AfterUpdate = ruleManager.Validate(company1);
        Console.WriteLine("Validar nif da company1 - nao deve passar porque agora é espanhola\n");
        if (validationErrors1AfterUpdate.Count == 0){
            Console.WriteLine("company 1 tem nif valido\n");
        }else{
            foreach (var error in validationErrors1AfterUpdate){
                Console.WriteLine($"o nif é para companys portuguesas\n");
            }
        } 
        // Criar leads
        Console.WriteLine("\n3. Criar Leads:");
        var lead1 = new Lead(company3, BusinessTypeEnum.Industry);
        var lead2 = new Lead(company2, BusinessTypeEnum.Retail);
        var lead3 = new Lead(company1, BusinessTypeEnum.FoodService);
        Console.WriteLine("\nLeads criados:");
        Console.WriteLine($"Lead 1: ID={lead1.LeadId}, Company ID={lead1.Company.ID}, Country={lead1.Country}, Business Type={lead1.BusinessType}, Status={lead1.Status}");
        Console.WriteLine($"Lead 2: ID={lead2.LeadId}, Company ID={lead2.Company.ID}, Country={lead2.Country}, Business Type={lead2.BusinessType}, Status={lead2.Status}");
        lead1.UpdateLeadBusinessType(BusinessTypeEnum.FoodService);
        Console.WriteLine("\nupdate business type do Lead 1 para Food:");
        Console.WriteLine($"Lead 1: ID={lead1.LeadId}, Company ID={lead1.Company.ID}, Country={lead1.Country}, Business Type={lead1.BusinessType}, Status={lead1.Status}");
        lead2.UpdateLeadStatus(StatusLeadEnum.Rejected);//TODO para status passar para accepted tem de criar primeiro a proposal e depois ele atualza sozinho por isso deve rejeitar
        Console.WriteLine("\nupdate status do Lead 2 para Rejected:");
        Console.WriteLine($"Lead 2: ID={lead2.LeadId}, Company ID={lead2.Company.ID}, Country={lead2.Country}, Business Type={lead2.BusinessType}, Status={lead2.Status}");
        Console.WriteLine("\nupdate company do Lead 1 para Company 1:");
        
        // Converter leads em propostas
        //Console.WriteLine("\n4. Converter Leads em Propostas:");
        //var proposal1 = lead1.leadToProposal();
        //var proposal2 = lead2.leadToProposal();
        //var proposal3 = lead2.leadToProposal(); //nao deve aceitar pq ja criou uma lead
        //Console.WriteLine("\nPropostas criadas a partir dos leads:");
        //Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        //Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}");
        //Console.WriteLine($"Proposal 3: ID={proposal3.ProposalID}, Lead ID={proposal3.Lead.LeadId}, Company ID={proposal3.Lead.Company.ID}, Products Count={proposal3.Products.Count}, Status={proposal3.Status}");
        //TODO o mesmo lead nao deve dar para criar mais do que uma proposta, entao verifica antes se status é diferente de accepted
        
        //Criar manualemnte uma proposal
        //Proposal proposal3 = new Proposal(lead1);
        //Console.WriteLine("\nProposal 3 criada manualmente a partir do Lead 1:");
        //Console.WriteLine($"Proposal 3: ID={proposal3.ProposalID}, Lead ID={proposal3.Lead.LeadId}, Company ID={proposal3.Lead.Company.ID}, Products Count={proposal3.Products.Count}, Status={proposal3.Status}");
        //TODO nao deve aceitar pq ja tem uma proposta criada a partir do lead1, entao tem de verificar o status do lead antes de criar a proposta, se for accepted nao deve criar a proposta
    }
/*
    
    

        // Criar produtos
        Console.WriteLine("\n2. Criando Produtos:");
        var product1 = new Product(ProductTypeEnum.Electronics, null);
        var product2 = new Product(ProductTypeEnum.Furniture, null);
        var product3 = new Product(ProductTypeEnum.Electronics, product2); // Produto dependente
        var product4 = new Product(ProductTypeEnum.Food, null);
        Console.WriteLine("\nProdutos criados:");
        Console.WriteLine($"Product 1: ID={product1.ProductID}, Type={product1.ProductType}, Dependent on Product ID={product1.DependentProduct?.ProductID}");
        Console.WriteLine($"Product 2: ID={product2.ProductID}, Type={product2.ProductType}, Dependent on Product ID={product2.DependentProduct?.ProductID}");
        Console.WriteLine($"Product 3: ID={product3.ProductID}, Type={product3.ProductType}, Dependent on Product ID={product3.DependentProduct?.ProductID}");
        Console.WriteLine($"Product 4: ID={product4.ProductID}, Type={product4.ProductType}, Dependent on Product ID={product4.DependentProduct?.ProductID}");
        product2.UpdateDependentProduct(product1);
        Console.WriteLine("\nupdate dependent product do Product 2 para Product 1:");
        Console.WriteLine($"Product 2: ID={product2.ProductID}, Type={product2.ProductType}, Dependent on Product ID={product2.DependentProduct?.ProductID}");
        product3.UpdateProductType(ProductTypeEnum.Furniture);
        Console.WriteLine("\nupdate product type do Product 3 para Furniture:");
        Console.WriteLine($"Product 3: ID={product3.ProductID}, Type={product3.ProductType}, Dependent on Product ID={product3.DependentProduct?.ProductID}");
    

        
    
        
        // Adicionar produtos às propostas
        Console.WriteLine("\n5. Adicionando Produtos às Propostas:");
        // Ordem correta: primeiro produtos sem dependências, depois os dependentes
        proposal1.AddProduct(product1); // Electronics, sem dependência
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Products Count={proposal1.Products.Count}");
        proposal1.AddProduct(product2); // Furniture, depende de product1 (Electronics) - mas tipos diferentes!
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Products Count={proposal1.Products.Count}");
        proposal1.AddProduct(product3); // Electronics, depende de product2 (Furniture) - mas tipos diferentes!
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Products Count={proposal1.Products.Count}");
        proposal1.AddProduct(product4); // Food, sem dependência
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Products Count={proposal1.Products.Count}");
        proposal2.AddProduct(product4); // Food, sem dependência
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Products Count={proposal2.Products.Count}");

        Console.WriteLine("\nTentando adicionar produto com dependência circular ou tipo errado:");
        var product5 = new Product(ProductTypeEnum.Furniture, product1); // Furniture depende de Electronics
        proposal1.AddProduct(product5); // Deve falhar pois tipos são diferentes

        Console.WriteLine("\nPropostas após adição de produtos:");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");
        Console.WriteLine($"Proposal 2: ID={proposal2.ProposalID}, Lead ID={proposal2.Lead.LeadId}, Company ID={proposal2.Lead.Company.ID}, Products Count={proposal2.Products.Count}, Status={proposal2.Status}");

        // Configurar detalhes das propostas
        Console.WriteLine("\n6. Configurando Detalhes das Propostas:");
        Console.WriteLine($"\nProposal 1 cost: {proposal1.ProductionCost}");
        Console.WriteLine($"\nProposal 2 cost: {proposal1.MonthlyProducedProducts}");
        Console.WriteLine($"\nProposal 1 expected profit: {proposal1.ExpectedMonthlyProfit}");
        Console.WriteLine("\nupdate production cost, monthly produced products e expected monthly profit da Proposal 1:");
        proposal1.ProductionCost = 5000.0;
        Console.WriteLine($"\nProposal 1 cost: {proposal1.ProductionCost}");
        proposal1.MonthlyProducedProducts = 100;
        proposal1.ExpectedMonthlyProfit = 2000.0;
        Console.WriteLine($"\nProposal 1 monthly produced products: {proposal1.MonthlyProducedProducts}");
        Console.WriteLine($"\nProposal 1 expected profit: {proposal1.ExpectedMonthlyProfit}");

        Console.WriteLine("\nupdate production cost, monthly produced products e expected monthly profit da Proposal 2:");
        proposal2.ProductionCost = 3000.0;
        proposal2.MonthlyProducedProducts = 50;
        proposal2.ExpectedMonthlyProfit = 1500.0;
        Console.WriteLine($"Company 2: {company2.Stakeholder}, Country: {company2.Country}, Status: {company2.Status}, NIF: {company2.NIF} ");
        Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");

        // Finalizar propostas
        Console.WriteLine("\n7. Finalizando Propostas:");
        proposal1.FinalizeProposal();
        proposal2.FinalizeProposal();

        // Mostrar resultados
        Console.WriteLine("\n8. Resultados Finais:");
                Console.WriteLine($"Proposal 1: ID={proposal1.ProposalID}, Lead ID={proposal1.Lead.LeadId}, Company ID={proposal1.Lead.Company.ID}, Products Count={proposal1.Products.Count}, Status={proposal1.Status}");

                Console.WriteLine($"Company 2: {company2.Stakeholder}, Country: {company2.Country}, Status: {company2.Status}, NIF: {company2.NIF} ");

    }
    
    /*

    // Configurar regras de validação dinâmica
    static void SetupValidationRules()
    {
        // Regra: Se Company.Country == "Portugal", então NIF é obrigatório
        SetRequired("Company", "NIF", "Company.Country == \"Portugal\"", true);
    }

    // Método para definir regras de validação
    static void SetRequired(string className, string fieldName, string condition, bool isRequired)
    {
        if (!validationRules.ContainsKey(className))
        {
            validationRules[className] = new Dictionary<string, (string, bool)>();
        }
        validationRules[className][fieldName] = (condition, isRequired);
        Console.WriteLine($"Regra definida: {fieldName} em {className} é {(isRequired ? "obrigatório" : "opcional")} quando {condition}");
    }

    // Validação dinâmica (simplificada)
    static bool ValidateField(string className, string fieldName, object obj)
    {
        if (!validationRules.ContainsKey(className) || !validationRules[className].ContainsKey(fieldName))
            return true;

        var (condition, isRequired) = validationRules[className][fieldName];

        // Simplificação: apenas verifica se Portugal requer NIF
        if (className == "Company" && fieldName == "NIF" && obj is Company company)
        {
            if (company.Country == "Portugal" && company.NIF == 0)
            {
                Console.WriteLine($"ERRO: NIF é obrigatório para empresas em Portugal");
                return false;
            }
        }

        return true;
    }

    // Company Management
    static Company CreateCompany(int nif, string address, string country, string stakeholder, string contact)
    {
        var company = new Company(nif, address, country, stakeholder, contact);
        if (ValidateField("Company", "NIF", company))
        {
            Console.WriteLine($"Empresa criada: ID={company.ID}, País={company.Country}, Status={company.Status}");
            return company;
        }
        throw new InvalidOperationException("Falha na validação da empresa");
    }

    // Product Management
    static Product CreateProduct(ProductTypeEnum productType, Product? dependentProduct)
    {
        var product = new Product(productType, dependentProduct);
        Console.WriteLine($"Produto criado: ID={product.ProductID}, Tipo={product.ProductType}, Dependente={dependentProduct?.ProductID ?? 0}");
        return product;
    }

    // Lead Management
    static Lead CreateLead(Company company, BusinessTypeEnum businessType)
    {
        var lead = new Lead(company, businessType);
        Console.WriteLine($"Lead criado: ID={lead.LeadId}, Empresa={lead.Company.ID}, País={lead.Country}, Tipo={lead.BusinessType}, Status={lead.Status}");
        return lead;
    }

    // Proposal Management
    static void AddProductToProposal(Proposal proposal, Product product)
    {
        proposal.AddProduct(product);
        Console.WriteLine($"Produto {product.ProductID} adicionado à proposta {proposal.ProposalID}");
    }

    static void FinalizeProposal(Proposal proposal)
    {
        proposal.Status = ProposalStatusEnum.Approved;
        proposal.Lead.Company.Status = CompanyStatus.Active;
        Console.WriteLine($"Proposta {proposal.ProposalID} finalizada. Status da empresa {proposal.Lead.Company.ID} alterado para {proposal.Lead.Company.Status}");
    }

    // Métodos de exibição
    static void DisplayCompany(Company company)
    {
        Console.WriteLine($"Empresa: ID={company.ID}, NIF={company.NIF}, País={company.Country}, Status={company.Status}");
    }

    static void DisplayProposal(Proposal proposal)
    {
        Console.WriteLine($"Proposta: ID={proposal.ProposalID}, Lead={proposal.Lead.LeadId}, Empresa={proposal.Lead.Company.ID}, Produtos={proposal.Products.Count}, Status={proposal.Status}");
        Console.WriteLine($"  Custo Produção: {proposal.ProductionCost}, Produção Mensal: {proposal.MonthlyProducedProducts}, Lucro Esperado: {proposal.ExpectedMonthlyProfit}");
    }
    */
}
