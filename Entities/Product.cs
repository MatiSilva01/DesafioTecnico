public class Product {
    private static int counter = 0;
    private int productID = 0;
    private Product? dependentProduct;//DependentProduct: Reference to another product (if dependent); de quem é dependente (tipo produto pai)
    private ProductTypeEnum productType;//ProductType: Type/category of the product (e.g., Electronics, Furniture)
    private List<Product> dependentProducts = new List<Product>(); //lista de produtos que são dependentes deste produto 

    //CONSTRUTOR
    public Product(ProductTypeEnum productType, Product? dependentProduct = null){
    if (dependentProduct != null && productType != dependentProduct.ProductType){
        throw new InvalidOperationException("Dependent product must have the same ProductType");
    }
    if (dependentProduct != null){ //se é dependente de outro produto
        dependentProduct.setDependetProducts(this); // adicionar este produto à lista de produtos dependentes do produto de que é dependente
    }
    this.productID = counter++;
    this.dependentProduct = dependentProduct;
    this.productType = productType;
}
    public int ProductID {
        get { return productID; }
    }

    public Product? DependentProduct {
        get { return dependentProduct; }
    }

    public ProductTypeEnum ProductType {
        get { return productType; }
    }
    public List<Product> DependentProducts {
        get { return dependentProducts; }
    }
    public void setDependetProducts(Product product) {
        if (!dependentProducts.Contains(product)) {
            dependentProducts.Add(product);
        }
    }
    public void removeDependentProduct(Product product) {
        dependentProducts.Remove(product);
    }

    public void UpdateProductType(ProductTypeEnum productType) {
        if (dependentProducts.Count > 0) { //se tem produtos dependetes (ou seja se tem filhos) nao pode atualizar otipo
                throw new InvalidOperationException("Cannot change ProductType: other products depend on this product.");
        }else {// se nao tem produtos que dependem dele (ou seja nao é pai de nenhum)
            if (dependentProduct != null && productType != dependentProduct.ProductType){ //se é depente de outro produto (é filho)
                    throw new InvalidOperationException("Cannot change ProductType: must match dependent product type.");
            } else { //se nao é dependente de outro produto (nao é filho) e nao tem produtos que dependem dele (nao é pai) pode atualizar o tipo do produto para qualquer tipo
                this.productType = productType;
            }
        }
    }
    public void UpdateDependentProduct(Product? newDependentProduct) { //atualiza ou define de quem é dependente (se é filho de que produto)
        if (newDependentProduct != null && productType != newDependentProduct.ProductType){
            throw new InvalidOperationException("Dependent product must have the same ProductType");//se quer definir que e dependente de outro produto, tem que ser do mesmo tipo
        }        
        // Remover do pai antigo (se houver)
        if (dependentProduct != null) {
            dependentProduct.removeDependentProduct(this);
        }
        // Atualizar para novo pai
        this.dependentProduct = newDependentProduct;
        // Adicionar ao novo pai (se houver)
        if (newDependentProduct != null) {
            newDependentProduct.setDependetProducts(this);
        }
    }
}
