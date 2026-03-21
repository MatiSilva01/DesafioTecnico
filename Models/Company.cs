class Company {
    private static int counter = 0;
    private int id = 0;//ID: Unique identifier
    private int nif;//● NIF: Tax Identification Number (required depending on country)
    private string address;//● Address: Company address
    private string country;//● Country: Country where the company is based
    private CompanyStatus status;//● Status: Company status (e.g., Draft, Active)
    private string stakeholder;//● Stakeholder: Person responsible for the company
    private string contact;//● Contact: Company contact information
    enum CompanyStatus
    {
        Draft,      // Empresa criada, mas ainda não contactada/sem negocios
        Active,     // Cliente ativo, esta a ser 
        //TODO sera que os dois a baixo fazem sentido?
        //Inactive,   // Já foi cliente mas no momento nao é
        //Blacklisted // Bloqueado, não fazer negócio
    }

    //CONSTRUCTOR
    public Company(int nif, string address, string country, string stakeholder, string contact) {
        this.id = counter++;
        this.nif = nif;
        this.address = address;
        this.country = country;
        this.status = CompanyStatus.Draft; 
        this.stakeholder = stakeholder;
        this.contact = contact;
    }

    //GETTERS AND SETTERS
    public int ID {
        get { return id; }
    }
    public int NIF {
        get { return nif; }
        set { nif = value; }
    }
    public string Address {
        get { return address; }
        set { address = value; }    
    }
    public string Country {
        get { return country; }
        set { country = value; }
    }
    public CompanyStatus Status {
        get { return status; }
        set { status = value; }
    }
    public string Stakeholder {
        get { return stakeholder; }
        set { stakeholder = value; }
    }
    public string Contact {
        get { return contact; }
        set { contact = value; }
    }

    //atualizar informacoes da empresa
    public void UpdateCompanyNIF(int nif) {
        this.nif = nif;
    }
    public void UpdateCompanyAddress(string address) {
        this.address = address;
    }
    public void UpdateCompanyCountry(string country) {
        this.country = country;
    }
    public void UpdateCompanyStakeholder(string stakeholder) {
        this.stakeholder = stakeholder;
    }
    public void UpdateCompanyContact(string contact) {
        this.contact = contact;
    }
    public void UpdateCompanyStatus(CompanyStatus status) {
        this.status = status;
    }
}