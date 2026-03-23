class Company {
    private static int counter = 0;
    private int id = 0;//ID: Unique identifier
    private int? nif;//● NIF: Tax Identification Number (required depending on country)
    private string address;//● Address: Company address
    private Country country;//● Country: Country where the company is based
    private CompanyStatus status;//● Status: Company status (e.g., Draft, Active)
    private string stakeholder;//● Stakeholder: Person responsible for the company
    private string contact;//● Contact: Company contact information

    public Company( string address, Country country, string stakeholder, string contact, int? nif = null) {
        this.id = counter++;
        this.nif = nif;
        this.address = address;
        this.country = country;
        this.status = CompanyStatus.Draft; 
        this.stakeholder = stakeholder;
        this.contact = contact;
    }

    public int ID {
        get { return id; }
    }
    public int? NIF {
        get { return nif; }
    }
    public string Address {
        get { return address; }
    }
    public Country Country {
        get { return country; }
    }
    public CompanyStatus Status {
        get { return status; }
    }
    public string Stakeholder {
        get { return stakeholder; }
    }
    public string Contact {
        get { return contact; }
    }

    //atualizar informacoes da empresa
    public void UpdateCompanyNIF(int nif) { //regra se for para portugal
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