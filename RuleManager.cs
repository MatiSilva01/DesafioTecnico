public class RuleManager{
    private List<Rule> rules;
    
    public RuleManager() {
        this.rules = new List<Rule>();
    }

    public void SetRequired(string className, string fieldName, Func<object, bool> condition, bool isRequired) {
        rules.Add(new Rule(className, fieldName, condition, isRequired));
    }


    public List<String> ValidateRules(object entity){ 
        if (entity == null) {
            throw new ArgumentNullException("Entity to validate cant be null.");
        }
        List<String> validationErrors = new List<String>();
        string className = entity.GetType().Name; //obter o nome da classe do objeto, para comparar com as regras
        foreach (Rule rule in rules){
            if (rule.ClassName == className ) { //verificar se a regra se aplica à classe do objeto 
                //verificar se a condicao e cumprida
                bool conditionResult = rule.Condition(entity); //chamar a funcao da condicao da regra //ex: para a regra do NIF obrigatório para empresas em Portugal, a funcao vai verificar se o objeto é uma empresa e se o país da empresa é Portugal
                if (conditionResult){//se a condicao e cumprida Ex:pais e pt
                    if (rule.IsRequired){ //sverificar se o campor e obrigatio (no caso sim e necessario nif)
                        //verificar se o campo tem valor
                        //var propertyInfo = entity.GetType()//o tipo (no caso do nif seria company)
                        var propertyInfo = entity.GetType().GetProperty(rule.FieldName); //obter a informacao do campo (exemplo nome dele e nif, é uma string )
                        if (propertyInfo != null) {
                            var fieldValue = propertyInfo.GetValue(entity); //obter o valor do campo (exemplo o valor do nif da empresa)
                            if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString())){ //verificar se o valor do campo é nulo ou vazio (casos por exemplo em que stakeholder e "" daria vazio e nao null)
                                validationErrors.Add($"Error: {rule.FieldName} is required for {className}.");
                            }
                        } else {
                            validationErrors.Add($"Error: Property '{rule.FieldName}' not found in {className}.");
                        }
                        
                    }
                }
                
            }
        }
    
        return validationErrors;
    }
    public void removeRule(Rule rule){
        this.rules.Remove(rule);
    }
    public void clearRules(){
        this.rules.Clear();
    }
    public List<Rule> getRules(){
        return rules;
    }

    //a condicao que queremos implementar (ou seja a funcao que vai em condition)
    public static bool CompanyIsInPortugal(object obj){
        Company company = (Company)obj; 
        //verificar se a company é portuguesa
        return company.Country == Country.Portugal; 
    }

    public static bool StakeholderIsRequired(object obj){
        Company company = (Company)obj;
        return (string.IsNullOrEmpty(company.Stakeholder));
    }
    
}
