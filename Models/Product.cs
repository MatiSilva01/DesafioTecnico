class Product {
    private int productID;//ProductID: Unique identifier
    private Product dependentProduct;//DependentProduct: Reference to another product (if dependent)
    private string productType;//ProductType: Type/category of the product (e.g., Electronics, Furniture)

    //CONSTRUTOR
    public Product(int productID, Product dependentProduct, string productType) {
        this.productID = productID;
        this.dependentProduct = dependentProduct;
        this.productType = productType;
    }
    //getters e setters
    public int ProductID() {
        get; set;
    }

    public Product DependentProduct() {
        get; set;
    }

    public string ProductType() {
        get; set;
    }
}
