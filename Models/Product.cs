class Product {
    private static int counter = 0;
    private int productID = 0;
    private Product? dependentProduct;//DependentProduct: Reference to another product (if dependent)
    private ProductTypeEnum productType;//ProductType: Type/category of the product (e.g., Electronics, Furniture)

    //CONSTRUTOR
    public Product(ProductTypeEnum productType, Product? dependentProduct = null){
    if (dependentProduct != null && productType != dependentProduct.ProductType){
        throw new InvalidOperationException("Dependent product must have the same ProductType");
    }
    this.productID = counter++;
    this.DependentProduct = dependentProduct;
    this.ProductType = productType;
}
    //getters e setters
    public int ProductID {
        get { return productID; }
    }

    public Product? DependentProduct {
        get { return dependentProduct; }
        set { dependentProduct = value; }
    }

    public ProductTypeEnum ProductType {
        get { return productType; }
        set { 
            if (dependentProduct != null && value != dependentProduct.ProductType){
                throw new InvalidOperationException("Cannot change ProductType: must match dependent product type.");
            }
            productType = value; 
        }
    }

    public void UpdateProductType(ProductTypeEnum productType) {
        this.productType = productType;
    }
    public void UpdateDependentProduct(Product dependentProduct) {
        this.dependentProduct = dependentProduct;
    }
}
