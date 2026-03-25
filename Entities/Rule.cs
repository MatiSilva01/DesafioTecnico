public class Rule
{
    private string className; //className: Name of the class (e.g., "Company")
    private string fieldName; // fieldName: Name of the field (e.g., "NIF")
    private Func<object, bool> condition; 
    private bool isRequired; // isRequired: Boolean flag (true se obrigatório, false se não)
   
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

    public Rule(string className, string fieldName, Func<object, bool> condition, bool isRequired) {
        this.className = className;
        this.fieldName = fieldName;
        this.condition = condition;
        this.isRequired = isRequired;
    }
}