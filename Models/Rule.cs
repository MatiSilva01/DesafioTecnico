class Rule{//cada regra deve ter o nome da classe, o campo onde se verifica a condicao (ex: NIF), a condicao (ex: Country == "Portugal") e se o campo é obrigatorio ou nao
    private string className; //className: Name of the class (e.g., "Company")
    private string fieldName; // fieldName: Name of the field (e.g., "NIF")
    //usamos um delegate em c# que permite referenciar um método como uma variável, e assim podemos passar a condição como um método que retorna um booleano, e esse método pode acessar os campos da classe para avaliar a condição
    private Func<object, bool> condition;
    //private string condition; //nao faz sentido ser uma string porque depois não conseguimos interpretar a condicao //● condition: Conditional expression (e.g., Company.Country == "Portugal")
    private bool isRequired; //● isRequired:Boolean flag (true if required, false otherwise)
   
    public string ClassName {
        get { return className; }
        set { className = value; }
    }
    public string FieldName {
        get { return fieldName; }
        set { fieldName = value; }
    }
    public Func<object, bool> Condition {
        get { return condition; }
        set { condition = value; }
    }
    public bool IsRequired {
        get { return isRequired; }
        set { isRequired = value; }
    }
    //esta faz sentido sequer? nao é so uma repeticao?? TODO - é o construtor da classe, nao é uma repeticao, é para criar as regras de validacao
    public Rule(string className, string fieldName, Func<object, bool> condition, bool isRequired) {
        this.className = className;
        this.fieldName = fieldName;
        this.condition = condition;
        this.isRequired = isRequired;
    }
}