class Product {
    private static int counter = 0;
    private int productID = 0;
    private Product dependentProduct;//DependentProduct: Reference to another product (if dependent)
    private ProductTypeEnum productType;//ProductType: Type/category of the product (e.g., Electronics, Furniture)
    enum ProductTypeEnum
    {
        Electronics,
        Furniture,
        Food,
        Clothing,
        Food
    }

    //CONSTRUTOR
    public Product(ProductTypeEnum productType, Product? dependentProduct = null){
    if (productType == null){
        throw new ArgumentNullException(nameof(productType), "Product must have a type.");
    }
    this.productID = counter++;
    this.DependentProduct = dependentProduct;
    this.ProductType = productType;
}
    //getters e setters
    public int ProductID {
        get { return productID; }
    }

    public Product DependentProduct {
        get { return dependentProduct; }
        set { dependentProduct = value; }
    }

    public ProductTypeEnum ProductType {
        get { return productType; }
        set { productType = value; }
    }

    public void UpdateProductInfo(Product dependentProduct, ProductTypeEnum productType) { //TODO verificar as funcoes update quando algum campo é null se ha stress, pq eu posso querer so atualizer um campo e pode ser estranho assim, talvez seja melhor criar funcoes de update para cada campo
        this.dependentProduct = dependentProduct;
        this.productType = productType;
    }
}
