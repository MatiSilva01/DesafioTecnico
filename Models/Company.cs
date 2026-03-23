class Company {
    private static int counter = 0;
    private int id = 0;//ID: Unique identifier
    private int? nif;//● NIF: Tax Identification Number (required depending on country)
    private string address;//● Address: Company address
    private Country country;//● Country: Country where the company is based
    private CompanyStatus status;//● Status: Company status (e.g., Draft, Active)
    private string stakeholder;//● Stakeholder: Person responsible for the company
    private string contact;//● Contact: Company contact information

    //CONSTRUCTOR
    public Company( string address, Country country, string stakeholder, string contact, int? nif = null) {
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
    public int? NIF {
        get { return nif; }
        set { nif = value; }
    }
    public string Address {
        get { return address; }
        set { address = value; }    
    }
    public Country Country {
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
    public void UpdateCompanyCountry(Country country) {
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