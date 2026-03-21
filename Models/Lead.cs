class Lead {
    private Company company;//Company: Reference to the associated company
    private static int counter = 0;
    private int leadId = 0;//LeadID: Unique identifier
    private string country;//Country: Inherited from the company
    private BusinessTypeEnum businessType;//BusinessType: Type of business (e.g., Industry, Retail)
    private string status;//Status: Status of the lead (e.g., Draft, Active)
    enum BusinessTypeEnum
    {
        Industry,
        Retail,
        Education,
        Technology,
        Healthcare,
        Finance,
        Defense,
        FoodService,
        Transportation,
        Energy
    }
    private enum StatusLeadEnum { 
        Draft,  //criado e nao avançou
        Accepted, //passou a proposal
        Rejected //rejeitado 
    }; // Company status (e.g., Draft, Active)
    private StatusLeadEnum status; // Current status

    //CONSTRUCTOR TODO
    public Lead(Company company, BusinessTypeEnum businessType) {
        if (company == null){
            throw new ArgumentNullException(nameof(company),"Lead must have an associated company.");
        }
        if (businessType == null){
            throw new ArgumentNullException(nameof(businessType),"Lead must have a business type.");
        }
        this.company = company;
        this.leadId = counter++;
        this.country = company.Country; //o contrutor esta mal porque o país é herdado da empresa
        this.businessType = businessType; 
        this.status = StatusLeadEnum.Draft; //construtor logo mete em draft
    }

    //GETTERS AND SETTERS
    public Company Company {
        get { return company; }
        set { company = value; } // TODO sera que faz sentido ter um setter para a empresa? talvez seja melhor só ter um getter, já que a empresa é definida no construtor e 
    }

    public int LeadId {
        get { return leadId; }
    }

    public string Country {
        get { return country; } //o país é herdado da empresa entao nao faz sentido o setter 
    }
    public BusinessTypeEnum BusinessType {
        get { return businessType; }
        set { businessType = value; }
    }
    public StatusLeadEnum Status {
        get { return status; }
        set { status = value; }
    }

    public void UpdateLeadInfo(Company company, BusinessTypeEnum businessType, StatusLeadEnum status) { //um update por informaçao nao? TODO
        this.company = company;
        this.country = company.Country; 
        this.businessType = businessType;
        this.status = status;
    }
    public Proposal leadToProposal() {
        return new Proposal(this);
    }
}