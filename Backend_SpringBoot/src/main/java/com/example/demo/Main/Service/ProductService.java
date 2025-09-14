package com.example.demo.Main.Service;


import com.example.demo.Main.Entity.Product;
import com.example.demo.Main.Repository.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ProductService {
    @Autowired
    private ProductRepository productRepository;


    //List All Prducts
    public List<Product> getAllProducts() {
        return productRepository.findAll();
    }


    public Product getProductById(String id) {
        return productRepository.findById(id).orElse(null);
    }

    public Product addProduct(Product product) {
        return productRepository.save(product);
    }
    public Product updateProduct(String id, Product updatedProduct) {
        return productRepository.findById(id).map(product -> {
            product.setName(updatedProduct.getName());
            product.setDescription(updatedProduct.getDescription());
            product.setPrice(updatedProduct.getPrice());
            product.setQuantity(updatedProduct.getQuantity());
            product.setImage(updatedProduct.getImage());
            return productRepository.save(product);
        }).orElse(null);
    }

    public void deleteProduct(String id) {
        productRepository.deleteById(id);
    }

}
