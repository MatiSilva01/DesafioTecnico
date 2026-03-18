class Proposal {
    private int proposalID;//ProposalID: Unique identifier
    private Lead lead;//● Lead: Reference to the associated lead
    private List<Product> products;//● Products: List of associated products
    private double productionCost;//● ProductionCost: Cost of production
    private int monthlyProducedProducts;//● MonthlyProducedProducts: Quantity produced monthly
    private double expectedMonthlyProfit;//● ExpectedMonthlyProfit: Estimated profit per month
    private string status;//● Status: Proposal status (e.g., Draft, Active)
    //● Inherits relevant fields from the associated Lead

    public Proposal(int proposalID, Lead lead, List<Product> products, double productionCost, int monthlyProducedProducts, double expectedMonthlyProfit, string status) {
        this.proposalID = proposalID;
        this.lead = lead;
        this.products = products;
        this.productionCost = productionCost;
        this.monthlyProducedProducts = monthlyProducedProducts;
        this.expectedMonthlyProfit = expectedMonthlyProfit;
        this.status = status;
    }

    public int ProposalID() {
        get; set;
    }
    public Lead Lead() {
        get; set;
    }
    public List<Product> Products() {
        get; set;
    }

    public double ProductionCost() {
        get; set;
    }

    public int MonthlyProducedProducts() {
        get; set;
    }
    public double ExpectedMonthlyProfit() {
        get; set;
    }
    public string Status() {
        get; set;
    }
}