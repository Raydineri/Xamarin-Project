package com.example.demo.Main.Service;

import com.example.demo.Main.Components.JwtUtils;
import com.example.demo.Main.Entity.User;
import com.example.demo.Main.Repository.UserRepository;
import com.example.demo.Main.Security.SecurityConfig;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;


@Service
public class AuthService {
    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;
    private final JwtUtils jwtUtils;
    private final SecurityConfig securityConfig;

    public AuthService(UserRepository userRepository, PasswordEncoder passwordEncoder , JwtUtils jwtUtils, SecurityConfig securityConfig) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
        this.jwtUtils = jwtUtils;
        this.securityConfig = securityConfig;
    }

    public User register(User user) {
        if(userRepository.findByUsername(user.getUsername()).isPresent())
        {
            throw new RuntimeException("User already exists");
        }
        if(userRepository.findByEmail(user.getEmail()).isPresent())
        {
            throw new RuntimeException("Email already exists");
        }
        User newUser = new User();
        newUser.setUsername(user.getUsername());
        newUser.setEmail(user.getEmail());
        newUser.setRole("ROLE_USER");
        newUser.setPassword( securityConfig.passwordEncoder().encode(user.getPassword()));
        return userRepository.save(newUser);
    }


    public String login(String email, String password) {
        Optional<User> user = userRepository.findByEmail(email);

        // Vérification si l'utilisateur existe et si le mot de passe est correct
        if (user.isEmpty() || !passwordEncoder.matches(password, user.get().getPassword())) {
            throw new RuntimeException("Invalid email or password");
        }

        // Récupération de l'ID utilisateur
        String userId = user.get().getId(); // Assurez-vous que 'getId' existe et retourne l'ID de l'utilisateur

        // Génération du token en incluant l'ID utilisateur
        return jwtUtils.generateToken(email, userId);
    }
}
