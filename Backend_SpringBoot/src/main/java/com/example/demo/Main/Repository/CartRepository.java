package com.example.demo.Main.Repository;

import com.example.demo.Main.Entity.CartItem;
import com.example.demo.Main.Entity.User;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.List;

public interface CartRepository extends MongoRepository<CartItem, Long> {
    List<CartItem> findByUser(User user);
}

