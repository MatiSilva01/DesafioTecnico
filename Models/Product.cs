class Product {
    private static int counter = 0;
    private int productID = 0;
    private Product dependentProduct;//DependentProduct: Reference to another product (if dependent)
    private string productType;//ProductType: Type/category of the product (e.g., Electronics, Furniture)

    //CONSTRUTOR
    public Product(Product dependentProduct, string productType) {
        this.productID = counter++;
        this.dependentProduct = dependentProduct;
        this.productType = productType;
    }
    //getters e setters
    public int ProductID {
        get; 
    }

    public Product DependentProduct {
        get; set;
    }

    public string ProductType {
        get; set;
    }

    public void UpdateProductInfo(Product dependentProduct, string productType) {
        this.dependentProduct = dependentProduct;
        this.productType = productType;
    }
}
