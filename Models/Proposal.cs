class Proposal {
    private static int counter = 0;
    private int proposalID = 0;//ProposalID: Unique identifier
    private Lead lead;//● Lead: Reference to the associated lead
    private List<Product> products;//● Products: List of associated products
    private double productionCost;//● ProductionCost: Cost of production
    private int monthlyProducedProducts;//● MonthlyProducedProducts: Quantity produced monthly
    private double expectedMonthlyProfit;//● ExpectedMonthlyProfit: Estimated profit per month
    private string status;//● Status: Proposal status (e.g., Draft, Active)
    //● Inherits relevant fields from the associated Lead

    public Proposal( Lead lead) {
        this.proposalID = counter++;
        this.lead = lead;
        this.products = new List<Product>();
        //this.productionCost = productionCost;
        //this.monthlyProducedProducts = monthlyProducedProducts;
        //this.expectedMonthlyProfit = expectedMonthlyProfit;
        this.status = "Draft";
    }

    public int ProposalID {
        get;
    }
    public Lead Lead {
        get; set;
    }
    public List<Product> Products {
        get; set;
    }

    public double ProductionCost {
        get; set;
    }

    public int MonthlyProducedProducts {
        get; set;
    }
    public double ExpectedMonthlyProfit {
        get; set;
    }
    public string Status {
        get; set;
    }

    public void UpdateProposalInfo(Lead lead, List<Product> products, double productionCost, int monthlyProducedProducts, double expectedMonthlyProfit, string status) {
        this.lead = lead;
        this.products = products;
        this.productionCost = productionCost;
        this.monthlyProducedProducts = monthlyProducedProducts;
        this.expectedMonthlyProfit = expectedMonthlyProfit;
        this.status = status;
    }

    public void AddProduct(Product product) {
        if (product.DependentProduct != null ) { //tem dependente
            if (products.Contains(product.DependentProduct)){//verificar se o produto dependente ja esta na lista
                product.productType = product.DependentProduct.ProductType; //o tipo do produto é o mesmo do produto dependente
                this.products.Add(product); //já tem o produto dependente, entao podemos adicionar o produto
            }
        }
        
    }

    public void FinalizeProposal() {
        if (lead != null && lead.Company != null){
            Company company = lead.Company;
            company.Status = "Active";
        }
    }
}