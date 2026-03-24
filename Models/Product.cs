public class Product {
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
    this.dependentProduct = dependentProduct;
    this.productType = productType;
}
    //getters e setters
    public int ProductID {
        get { return productID; }
    }

    public Product? DependentProduct {
        get { return dependentProduct; }
    }

    public ProductTypeEnum ProductType {
        get { return productType; }
    }

    public void UpdateProductType(ProductTypeEnum productType) {
        if (dependentProduct != null && productType != dependentProduct.ProductType){
            throw new InvalidOperationException("Cannot change ProductType: must match dependent product type.");
        }
        this.productType = productType;
    }
    public void UpdateDependentProduct(Product dependentProduct) {
        this.dependentProduct = dependentProduct;
    }
}
