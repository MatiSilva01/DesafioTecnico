class Lead {
    private Company company;//Company: Reference to the associated company
    private static int counter = 0;
    private int leadId = 0;//LeadID: Unique identifier
    private string country;//Country: Inherited from the company
    private string businessType;//BusinessType: Type of business (e.g., Industry, Retail)
    private string status;//Status: Status of the lead (e.g., Draft, Active)

    //CONSTRUCTOR
    public Lead(Company company, string businessType, string status) {
        this.company = company;
        this.leadId = counter++;
        this.country = company.Country; 
        this.businessType = businessType; 
        this.status = status;
    }

    //GETTERS AND SETTERS
    public Company Company {
        get;set; //sera que faz sentido ter um setter para a empresa? talvez seja melhor só ter um getter, já que a empresa é definida no construtor 
    }

    public int LeadId {
        get;
    }

    public string Country {
        get; //o país é herdado da empresa entao nao faz sentido o setter 
    }
    public string BusinessType {
        get; set;
    }
    public string Status {
        get; set;
    }

    public void UpdateLeadInfo(Company company, string businessType, string status) {
        this.company = company;
        this.country = company.Country; 
        this.businessType = businessType;
        this.status = status;
    }
    public Proposal leadToProposal() {
        return new Proposal(this, new List<Product>(), 0, 0, 0, "Draft");
    }
}