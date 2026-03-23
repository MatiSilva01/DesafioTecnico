class Lead {
    private Company company;//Company: Reference to the associated company
    private static int counter = 0;
    private int leadId = 0;//LeadID: Unique identifier
    private Country country;//Country: Inherited from the company
    private BusinessTypeEnum businessType;//BusinessType: Type of business (e.g., Industry, Retail)
    private StatusLeadEnum status;//Status: Status of the lead (e.g., Draft, Active)
    //CONSTRUCTOR TODO
    public Lead(Company company, BusinessTypeEnum businessType) {
        if (company == null){
            throw new ArgumentNullException(nameof(company),"Lead must have an associated company.");
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

    public Country Country {
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
    //atualizar informacoes do lead
    public void UpdateLeadCompany(Company company) {
        this.company = company;
        this.country = company.Country; //atualizar o país quando se atualiza a empresa, porque o país é herdado da empresa
    }
    public void UpdateLeadBusinessType(BusinessTypeEnum businessType) {
        this.businessType = businessType;
    }
    public void UpdateLeadStatus(StatusLeadEnum status) {
        this.status = status;
    }
    public Proposal leadToProposal() {
        return new Proposal(this);
    }
}