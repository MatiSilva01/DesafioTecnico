class Proposal {
    private static int counter = 0;
    private int proposalID = 0;//ProposalID: Unique identifier
    private Lead lead;//● Lead: Reference to the associated lead
    private List<Product> products;//● Products: List of associated products
    private double productionCost;//● ProductionCost: Cost of production
    private int monthlyProducedProducts;//● MonthlyProducedProducts: Quantity produced monthly
    private double expectedMonthlyProfit;//● ExpectedMonthlyProfit: Estimated profit per month
    private ProposalStatusEnum status;//● Status: Proposal status (e.g., Draft, Active)
    //● Inherits relevant fields from the associated Lead

    public Proposal( Lead lead) {
        if (lead == null){
            throw new ArgumentNullException(nameof(lead),"Proposal must have an associated lead.");
        }
        this.proposalID = counter++;
        this.lead = lead;
        this.products = new List<Product>();
        //TODO verificar se faz sentido o construtor receber os produtos, o custo de produção, a quantidade produzida mensalmente e o lucro mensal esperado ou se isso devia ser definido depois, porque quando se cria a proposta ainda não se sabe quais os produtos que vão estar associados, nem o custo de produção, etc. Talvez seja melhor criar a proposta só com a lead e depois ir adicionando os produtos e as outras informações.
        //this.productionCost = productionCost;
        //this.monthlyProducedProducts = monthlyProducedProducts;
        //this.expectedMonthlyProfit = expectedMonthlyProfit;
        this.status = ProposalStatusEnum.Draft;
    }

    public int ProposalID {
        get { return proposalID; }
    }
    public Lead Lead {
        get { return lead; }
        set { lead = value; }
    }
    public List<Product> Products {
        get { return products; }
        //nao pus set, porque temos o add products
    }

    public double ProductionCost {
        get { return productionCost; }
        set { productionCost = value; }
    }

    public int MonthlyProducedProducts {
        get { return monthlyProducedProducts; }
        set { monthlyProducedProducts = value; }
    }
    public double ExpectedMonthlyProfit {
        get { return expectedMonthlyProfit; }
        set { expectedMonthlyProfit = value; }
    }
    public ProposalStatusEnum Status {
        get { return status; }
        set { status = value; }
    }

//TODO pensar melhor se faz sentido dado que muda a lead..
    public void UpdateProposalLead(Lead lead) {
        if (status == ProposalStatusEnum.Approved){
            Console.WriteLine("Error: Cannot update the lead of an approved proposal.");
            return;
        } else {
            this.lead = lead;
        }
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
            return; //nao adicionar o produto
        }
        if (product.DependentProduct != null ) { //se tem dependente
            if(products.Contains(product.DependentProduct)){//verificar se o produto dependente ja esta na lista
                //se o dependete ta na lista verifica se é do mesmo tipo
                if(product.ProductType != product.DependentProduct.ProductType){
                    Console.WriteLine($"Error: Cannot add Product ID={product.ProductID} because its type {product.ProductType} is different from its dependent product type {product.DependentProduct.ProductType}.");
                    return; //nao adicionar o produto
                }
                //product.ProductType = product.DependentProduct.ProductType; //o tipo do produto é o mesmo do produto dependente
            }else{//se o produto dependente nao esta na lista nao pode adicionar o produto 
                Console.WriteLine($"Error: Cannot add Product ID={product.ProductID} because its dependent product ID={product.DependentProduct.ProductID} is not in the list os products of the proposal.");
                return; //nao adicionar o produto
            }
        }
        products.Add(product); //adicionar o produto
    }
    public void RemoveProduct(Product product) {
        if (product == null) {
            Console.WriteLine("Error: Cannot remove a null product from the proposal.");
            return; //nao remover o produto
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
        Company company = lead.Company;
        company.Status = CompanyStatus.Active;
        this.status = ProposalStatusEnum.Approved;
    }
}