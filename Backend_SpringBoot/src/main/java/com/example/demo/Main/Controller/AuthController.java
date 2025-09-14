package com.example.demo.Main.Controller;


import com.example.demo.Main.Components.JwtUtils;
import com.example.demo.Main.Entity.User;
import com.example.demo.Main.Service.AuthService;
import com.example.demo.Main.Service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;
import java.util.Optional;

@RestController
@RequestMapping("/api/auth")
public class AuthController {
    @Autowired
    private UserService userService;
    @Autowired
    private JwtUtils jwtUtils;
    @Autowired
    private PasswordEncoder passwordEncoder;
    @Autowired
    private AuthService authService;


    // Register
    @PostMapping("/register")
    public ResponseEntity<?> registerUser(@RequestBody User user) {
        try{

        User RegisteredUser = authService.register(user);
        return ResponseEntity.ok(RegisteredUser);

    } catch (Exception e) {
        return ResponseEntity.badRequest().body(e.getMessage());
    }
    }



    // Login a user (example with basic username/password validation)
    @PostMapping("/login")
    public ResponseEntity<String> loginUser(@RequestBody User loginRequest) {
        Optional<User> user = userService.findByEmail(loginRequest.getEmail());

        if (user.isPresent() && passwordEncoder.matches(loginRequest.getPassword(), user.get().getPassword())) {
            // If authentication is successful, you can generate JWT here (if using JWT)
            String token = authService.login(loginRequest.getEmail(), loginRequest.getPassword());
            return ResponseEntity.ok(token);
        } else {
            return ResponseEntity.status(401).body("Invalid credentials");
        }
    }
    @PostMapping("/admin-login")
    public ResponseEntity<?> adminLogin(@RequestParam String email, @RequestParam String password) {
        if ("admin@gmail.com".equals(email) && "admin".equals(password)) {
            String token = jwtUtils.generateToken("admin", "ADMIN");
            Map<String, String> response = new HashMap<>();
            response.put("token", token);
            return ResponseEntity.ok(response);
        } else {
            return ResponseEntity.status(401).body("Invalid admin credentials");
        }
    }

}
