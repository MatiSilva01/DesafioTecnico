public class Lead { //oportunidade de negocio com uma dada empresa, mas pode ser convertido numa proposta real
    private Company company;//Company: Reference to the associated company
    private static int counterLead = 0;
    private int leadId = 0;//LeadID: Unique identifier
    private Country country;//Country: Inherited from the company
    private BusinessTypeEnum businessType;//BusinessType: Type of business (e.g., Industry, Retail)
    private StatusLeadEnum status;//Status: Status of the lead (e.g., Draft, Active)

    public Lead(Company company, BusinessTypeEnum businessType) {
        if (company == null){
            throw new ArgumentNullException(nameof(company),"Lead must have an associated company.");
        }
        this.company = company;
        this.leadId = counterLead++;
        this.country = company.Country; //o contrutor esta mal porque o país é herdado da empresa
        this.businessType = businessType; 
        this.status = StatusLeadEnum.Draft; //construtor logo mete em draft
    }

    //GETTERS AND SETTERS
    public Company Company {
        get { return company; }
    }

    public int LeadId {
        get { return leadId; }
    }

    public Country Country {
        get { return country; } 
    }
    public BusinessTypeEnum BusinessType {
        get { return businessType; }
    }
    public StatusLeadEnum Status {
        get { return status; }
    }
    //atualizar informacoes do lead
    public void UpdateLeadBusinessType(BusinessTypeEnum businessType) {
        this.businessType = businessType;
    }
    public void UpdateLeadStatus(StatusLeadEnum status) { 
        this.status = status;
    }

    public Proposal leadToProposal() {
        if (this.status == StatusLeadEnum.Accepted){
            throw new InvalidOperationException($"Cannot convert Lead ID={this.LeadId} to Proposal because there's an existing proposal associated with this lead.");
        }
        return new Proposal(this);
    }
}