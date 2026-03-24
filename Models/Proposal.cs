public class Proposal {
    private static int counter = 0;
    private int proposalID = 0;//ProposalID: Unique identifier
    private Lead lead;//● Lead: Reference to the associated lead
    private List<Product> products;//● Products: List of associated products
    private double productionCost;//● ProductionCost: Cost of production
    private int monthlyProducedProducts;//● MonthlyProducedProducts: Quantity produced monthly
    private double expectedMonthlyProfit;//● ExpectedMonthlyProfit: Estimated profit per month
    private ProposalStatusEnum status;//● Status: Proposal status (e.g., Draft, Active)
    private Company company; 
    private Country country; 
    private int leadId;
    //● Inherits relevant fields from the associated Lead

    public Proposal( Lead lead) {
        if (lead == null){
            throw new ArgumentNullException(nameof(lead),"Proposal must have an associated lead.");
        }
        if (lead.Status == StatusLeadEnum.Accepted){
            Console.WriteLine($"Error: Cannot create a new proposal for Lead ID={lead.LeadId} because there's an existing proposal associated with this lead.");
            return; //nao criar a proposta TODo verificar se deveria ser
        }
        this.proposalID = counter++;
        this.lead = lead;
        this.products = new List<Product>();
        this.company = lead.Company;
        this.country = lead.Country;
        //TODO verificar se faz sentido o construtor receber os produtos, o custo de produção, a quantidade produzida mensalmente e o lucro mensal esperado ou se isso devia ser definido depois, porque quando se cria a proposta ainda não se sabe quais os produtos que vão estar associados, nem o custo de produção, etc. Talvez seja melhor criar a proposta só com a lead e depois ir adicionando os produtos e as outras informações.
        this.status = ProposalStatusEnum.Draft;
    }

    public int ProposalID {
        get { return proposalID; }
    }
    public Lead Lead {
        get { return lead; }
    }
    public List<Product> Products {
        get { return products; }
    }

    public double ProductionCost {
        get { return productionCost; }
    }

    public int MonthlyProducedProducts {
        get { return monthlyProducedProducts; }
    }
    public double ExpectedMonthlyProfit {
        get { return expectedMonthlyProfit; }
    }
    public ProposalStatusEnum Status {
        get { return status; }
    }


    public void UpdateProposalProductionCost(double productionCost) {
        if (status == ProposalStatusEnum.Approved){
            Console.WriteLine("Error: Cannot update the production cost of an approved proposal.");
            return;
        } else {
            this.productionCost = productionCost;
        }
    }
    public void UpdateProposalMonthlyProducedProducts(int monthlyProducedProducts) {
        if (status == ProposalStatusEnum.Approved){
            Console.WriteLine("Error: Cannot update the monthly produced products of an approved proposal.");
            return; 
        } else {
            this.monthlyProducedProducts = monthlyProducedProducts;
        }
    }
    public void UpdateProposalExpectedMonthlyProfit(double expectedMonthlyProfit) {
        if (status == ProposalStatusEnum.Approved){
            Console.WriteLine("Error: Cannot update the expected monthly profit of an approved proposal.");
            return; 
        } else {
            this.expectedMonthlyProfit = expectedMonthlyProfit;
        }
    }
    public void UpdateProposalStatus(ProposalStatusEnum status) {
        if (this.status == ProposalStatusEnum.Approved){
            Console.WriteLine("Error: Cannot update the status of an approved proposal.");
            return; 
        } else {
            this.status = status;
        }
    }

    public void AddProduct(Product product) {
        if (product == null) {
            Console.WriteLine("Error: Cannot add a null product to the proposal.");
            return; //nao adicionar o produto //TODO voltar a pensar se este if faz sentido
        }
        if (product.DependentProduct != null ) { //se é dependente de outro produto
            if(products.Contains(product.DependentProduct)){//verificar se o produto de que é dependente ja esta na lista
                //se o produto de que e dependete ta na lista verifica se é do mesmo tipo
                if(product.ProductType != product.DependentProduct.ProductType){
                    Console.WriteLine($"Error: Cannot add Product ID={product.ProductID} because its type {product.ProductType} is different from its dependent product type {product.DependentProduct.ProductType}.");
                    return; //nao adicionar o produto
                }
            }else{//se o produto de que é dependente nao esta na lista nao pode adicionar o produto 
                Console.WriteLine($"Error: Cannot add Product ID={product.ProductID} because its dependent product ID={product.DependentProduct.ProductID} is not in the list os products of the proposal.");
                return; 
            }
        }
        products.Add(product); 
    }
    public void RemoveProduct(Product product) {
        if (product == null) {
            Console.WriteLine("Error: Cannot remove a null product from the proposal.");
            return; //nao remover o produto //TODO voltar a pensar se este if faz sentido
        }
        if (products.Contains(product)) {
            products.Remove(product); //remover o produto
        } else {
            Console.WriteLine($"Error: Cannot remove Product ID={product.ProductID} because it is not in the list of products of the proposal.");
        }
    }

    public void FinalizeProposal() {
        if (products.Count < 1){
            Console.WriteLine("Error: Cannot finalize the proposal because it has no products.");
            return; 
        }
        if (productionCost <= 0){
            Console.WriteLine("Error: Cannot finalize the proposal because the production cost is not defined or is less than or equal to zero.");
            return;
        }       
        if (monthlyProducedProducts <= 0){
            Console.WriteLine("Error: Cannot finalize the proposal because the monthly produced products is not defined or is less than or equal to zero.");
            return;
        }
        if (expectedMonthlyProfit <= 0){
            Console.WriteLine("Error: Cannot finalize the proposal because the expected monthly profit is not defined or is less than or equal to zero.");
            return;
        }
        this.status = ProposalStatusEnum.Approved;
        Company company = lead.Company;
        company.ActivateCompany(this);
    }
}