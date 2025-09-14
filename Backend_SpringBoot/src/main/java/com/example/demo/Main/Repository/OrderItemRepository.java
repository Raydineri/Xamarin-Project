package com.example.demo.Main.Repository;

import com.example.demo.Main.Entity.OrderItem;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface OrderItemRepository extends MongoRepository<OrderItem , String> {
}
