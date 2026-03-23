class RuleManager{ //vai guardar todas as regras de validacao, e vai ser responsavel por validar as regras, ou seja, avaliar as condicoes das regras e verificar se os campos estao preenchidos ou nao
    private List<Rule> rules; // List of rules
    
    public RuleManager() {
        this.rules = new List<Rule>();
    }

    public void AddRule(Rule rule){
        this.rules.Add(rule);
    }
    public bool isFieldRequired(string className, string fieldName){
        foreach (Rule rule in rules){
            if (rule.ClassName == className && rule.FieldName == fieldName){
                return rule.IsRequired;
            }
        }
        return false; 
    }
    public List<String> Validate(object entity){ 
        List<String> validationErrors = new List<String>();
        string className = entity.GetType().Name; //obter o nome da classe do objeto, para comparar com as regras
        foreach (Rule rule in rules){
            if (rule.ClassName == className && rule.Condition(entity)) { //verificar se a regra se aplica à classe do objeto e se a condição da regra é satisfeita para o objeto
                //vamos buscar do campo que 
                //entity.GetType() //obter o tipo do objeto, no caso a classe Company
                //entity.GetType().GetProperty(rule.FieldName) //obter a propriedade do objeto que corresponde ao nome do campo da regra, por exemplo a propriedade NIF
                //entity.GetType().GetProperty(rule.FieldName).GetValue(entity) //obter o valor do campo do objeto, no caso o valor do NIF da empresa
                var prop = entity.GetType().GetProperty(rule.FieldName);
                var fieldValue = prop != null ? prop.GetValue(entity) : null;
                if (fieldValue == null || fieldValue.ToString() == "-1" || string.IsNullOrEmpty(fieldValue.ToString())){ //verificar se o campo esta vazio ou nulo
                    if (rule.IsRequired){ //se o campo é obrigatório, mas está vazio, então é um erro de validação
                        validationErrors.Add($"Field {rule.FieldName} is required for class {rule.ClassName}.");
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
    //TODO nao me parece tar bem
    public List<Rule> GetRules(){
        return this.rules;
    }

    public static bool CompanyIsInPortugal(object obj){
        Company company = (Company)obj; //fazer um cast do objeto para a classe Company, para acessar o campo Country
        return company.Country == Country.Portugal; //verificar se o campo Country da empresa é igual a Portugal, e retornar true ou false
    }

    /*public static void ApplyRules(Proposal proposal)
    {
        if (proposal.ProductionCost > 4000.0){
            proposal.Status = ProposalStatusEnum.Rejected;
        }else if (proposal.MonthlyProducedProducts < 10){
            proposal.Status = ProposalStatusEnum.Rejected;
        }else if (proposal.ExpectedMonthlyProfit < 1000.0){
            proposal.Status = ProposalStatusEnum.Rejected;
        }else{
            proposal.Status = ProposalStatusEnum.Approved;
        }
    }*/
}
