package com.example.demo.Main.Controller;


import com.example.demo.Main.Entity.Category;
import com.example.demo.Main.Service.CategoryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/Category")
public class CategoryController {
    @Autowired
    CategoryService categoryService;


    @GetMapping("/Categories")
    public List<Category> getAllCategories() {
        return categoryService.getAllCategories();

    }
    @PostMapping("/addCategory")
    public Category addCategory(@RequestBody Category category) {
        return categoryService.addCategory(category);
    }

    @PutMapping("/updateCategory/{id}")
    public Category updateCategory(@PathVariable String id,@RequestBody Category updatedCategory) {
        return categoryService.updateCategory(id, updatedCategory);
    }
    @DeleteMapping("/deleteCategory/{id}")
    public void deleteCategory(@PathVariable String id) {
        categoryService.deleteCategory(id);
    }






}
