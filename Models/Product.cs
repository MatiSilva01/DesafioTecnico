class Product {
    private static int productID = 0;
    private Product dependentProduct;//DependentProduct: Reference to another product (if dependent)
    private string productType;//ProductType: Type/category of the product (e.g., Electronics, Furniture)

    //CONSTRUTOR
    public Product(Product dependentProduct, string productType) {
        this.productID = productID++;
        this.dependentProduct = dependentProduct;
        this.productType = productType;
    }
    //getters e setters
    public int ProductID() {
        get; 
    }

    public Product DependentProduct() {
        get; set;
    }

    public string ProductType() {
        get; set;
    }
}
