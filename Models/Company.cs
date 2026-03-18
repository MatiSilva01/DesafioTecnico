class Company {
    private int id;//ID: Unique identifier
    private int nif;//● NIF: Tax Identification Number (required depending on country)
    private string address;//● Address: Company address
    private string country;//● Country: Country where the company is based
    private string status;//● Status: Company status (e.g., Draft, Active)
    private string stakeholder;//● Stakeholder: Person responsible for the company
    private string contact;//● Contact: Company contact information

    //CONSTRUCTOR
    public Company(int id, int nif, string address, string country, string status, string stakeholder, string contact) {
        this.id = id;
        this.nif = nif;
        this.address = address;
        this.country = country;
        this.status = status;
        this.stakeholder = stakeholder;
        this.contact = contact;
    }

    //GETTERS AND SETTERS
    public int ID() {
        get { return id; }
        set { id = value; }
    }
    public int NIF() {
        get { return nif; }
        set { nif = value; }
    }
    public string Address() {
        get { return address; }
        set { address = value; }    
    }
    public string Country() {
        get { return country; }
        set { country = value; }
    }
    public string Status() {
        get { return status; }
        set { status = value; }
    }
    public string Stakeholder() {
        get { return stakeholder; }
        set { stakeholder = value; }
    }
    public string Contact() {
        get { return contact; }
        set { contact = value; }
    }

}