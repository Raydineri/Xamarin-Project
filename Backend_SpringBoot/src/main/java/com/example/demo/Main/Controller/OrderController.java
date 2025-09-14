package com.example.demo.Main.Controller;

import com.example.demo.Main.Entity.Order;
import com.example.demo.Main.Service.OrderService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController

    @RequestMapping("/api/orders")
    public class OrderController {

        @Autowired
        private OrderService orderService;

        @PostMapping
        public ResponseEntity<Order> createOrder(@RequestBody Order order) {
            try {
                Order createdOrder = orderService.createOrder(order);
                return ResponseEntity.ok(createdOrder);
            } catch (Exception e) {
                return ResponseEntity.status(500).body(null);  // Retourner une erreur interne en cas d'exception
            }
        }
    @GetMapping
    public ResponseEntity<List<Order>> getAllOrders() {
        return ResponseEntity.ok(orderService.getAllOrders());
    }

    @GetMapping("/{id}")
    public ResponseEntity<Order> getOrderById(@PathVariable String id) {
        return ResponseEntity.ok(orderService.getOrderById(id));
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteOrder(@PathVariable String id) {
        orderService.deleteOrder(id);
        return ResponseEntity.noContent().build();
    }
}
