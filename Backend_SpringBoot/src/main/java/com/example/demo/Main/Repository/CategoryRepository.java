package com.example.demo.Main.Repository;

import com.example.demo.Main.Entity.Category;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

import javax.swing.text.html.Option;
import java.util.Optional;
@Repository
public interface CategoryRepository extends MongoRepository<Category, String> {
    Optional <Category> findByName(String name);

}
