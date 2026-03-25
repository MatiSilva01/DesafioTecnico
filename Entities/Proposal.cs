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
    //● Inherits relevant fields from the associated Lead

    public Proposal( Lead lead) {
        if (lead == null){
            throw new ArgumentNullException(nameof(lead),"Proposal must have an associated lead.");
        }
        if (lead.Status == StatusLeadEnum.Accepted){
            throw new InvalidOperationException($"Cannot create a new proposal for this Lead because there's an existing proposal associated with this lead.");
        }   
        lead.UpdateLeadStatus(StatusLeadEnum.Accepted);
        this.proposalID = counter++;
        this.lead = lead;
        this.products = new List<Product>();
        this.company = lead.Company;
        this.country = lead.Country;
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
    public Company Company {
        get { return company; }
    }
    public Country Country {
        get { return country; }
    }



    public void UpdateProposalProductionCost(double productionCost) {
        if (status == ProposalStatusEnum.Approved){
            throw new InvalidOperationException("Cannot update the production cost of an approved proposal.");
        } 
        if (productionCost <= 0){
            throw new InvalidOperationException("Error: Production cost must be greater than zero.");
        }
        this.productionCost = productionCost;  
    }
    public void UpdateProposalMonthlyProducedProducts(int monthlyProducedProducts) {
        if (status == ProposalStatusEnum.Approved){
            throw new InvalidOperationException("Error: Cannot update the monthly produced products of an approved proposal.");
        } 
        if (monthlyProducedProducts <= 0){
            throw new InvalidOperationException("Error: Monthly produced products must be greater than zero.");
        }
            this.monthlyProducedProducts = monthlyProducedProducts;
    }
    public void UpdateProposalExpectedMonthlyProfit(double expectedMonthlyProfit) {
        if (status == ProposalStatusEnum.Approved){
            throw new InvalidOperationException("Error: Cannot update the expected monthly profit of an approved proposal.");
        } if (expectedMonthlyProfit < 0){
            throw new InvalidOperationException("Error: Expected monthly profit must be greater than zero.");
        } 
            this.expectedMonthlyProfit = expectedMonthlyProfit;
    }
    public void UpdateProposalStatus(ProposalStatusEnum status) {
        if (status == ProposalStatusEnum.Approved){
            throw new InvalidOperationException("Error: Cannot change the proposal status to Approved manually because it should be updated in finalizeProposal.");
        }
            this.status = status;
    }

    public void AddProduct(Product product) {
        if (product == null) {
            throw new InvalidOperationException("Error: Cannot add a null product to the proposal.");
        }
        if (product.DependentProduct != null ) { //se é dependente de outro produto
            if(products.Contains(product.DependentProduct)){//verificar se o produto de que é dependente ja esta na lista
                //se o produto de que e dependete ta na lista verifica se é do mesmo tipo
                if(product.ProductType != product.DependentProduct.ProductType){
                    throw new InvalidOperationException($"Error: Cannot add Product ID={product.ProductID} because its type {product.ProductType} is different from its dependent product type {product.DependentProduct.ProductType}.");
                }
            }else{//se o produto de que é dependente nao esta na lista nao pode adicionar o produto 
                throw new InvalidOperationException($"Error: Cannot add Product ID={product.ProductID} because its dependent product ID={product.DependentProduct.ProductID} is not in the list os products of the proposal.");
            }
        }
        products.Add(product); 
    }
    public void RemoveProduct(Product product)
    {
        if (product == null)
            throw new InvalidOperationException("Error: Cannot remove a null product from the proposal.");

        if (!products.Contains(product))
            throw new InvalidOperationException(
                $"Error: Cannot remove Product ID={product.ProductID} because it is not in the list of products of the proposal.");

        bool hasDependentsInProposal = products.Any(p => p.DependentProduct == product);
        if (hasDependentsInProposal)
            throw new InvalidOperationException(
                "Error: Cannot remove a product because there are other products in the proposal that depend on it. Should remove the dependent products first.");

        products.Remove(product);
    }

    public void FinalizeProposal() {
        if (products.Count < 1){
            throw new InvalidOperationException("Error: Cannot finalize the proposal because it has no products.");
        }
        if (productionCost <= 0){
            throw new InvalidOperationException("Error: Cannot finalize the proposal because the production cost is not defined or is less than or equal to zero.");
        }       
        if (monthlyProducedProducts <= 0){
            throw new InvalidOperationException("Error: Cannot finalize the proposal because the monthly produced products is not defined or is less than or equal to zero.");
        }
        if (expectedMonthlyProfit <= 0){
            throw new InvalidOperationException("Error: Cannot finalize the proposal because the expected monthly profit is not defined or is less than or equal to zero.");
        }
        this.status = ProposalStatusEnum.Approved;
        Company company = lead.Company;
        company.ActivateCompany(this);
    }
}