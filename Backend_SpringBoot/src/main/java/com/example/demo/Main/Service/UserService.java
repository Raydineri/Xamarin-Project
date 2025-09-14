package com.example.demo.Main.Service;

import com.example.demo.Main.Entity.User;
import com.example.demo.Main.Repository.UserRepository;
import com.example.demo.Main.Security.SecurityConfig;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;


@Service
public class UserService {


    @Autowired
    private UserRepository userRepository;

    @Autowired
    private SecurityConfig bCryptPasswordEncoder;


    // Find user by username
    public Optional<User> findByUsername(String username) {
        return userRepository.findByUsername(username);
    }
    public Optional<User> findByEmail(String email) {
        return userRepository.findByEmail(email);
    }

    public User addUser(User user) {
        return userRepository.save(user);
    }
    public User updateUser(String id, User updatedUser) {
        return userRepository.findById(id).map(user -> {
            user.setUsername(updatedUser.getUsername());
            user.setEmail(updatedUser.getEmail());
            user.setPassword(bCryptPasswordEncoder.passwordEncoder().encode(updatedUser.getPassword()));
            return userRepository.save(user);
        }).orElse(null);
    }

    public void deleteUser(String id) {
        userRepository.deleteById(id);
    }
    public User getUserById(String id) {
        return userRepository.findById(id).orElse(null);
    }


}
