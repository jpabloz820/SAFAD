package com.safad.safad.Models.Entity;

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.Table;
import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.Size;
import javax.validation.constraints.Email;

@Entity
@Table(name = "people")

public class People
{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long Id_People;
    
    @NotEmpty (message = "Documento invalido o vacío")
    @Size(min = 8, max = 11)
    private String Document;

    @NotEmpty (message = "Nombre invalido o vacío")
    @Size(min = 5, max = 20)
    public String Name;

    @NotEmpty (message = "Dirección invalida o vacío")
    @Size(min = 10)
    public String Adress;

    @NotEmpty (message = "Teléfono invalido o vacío")
    @Size(min = 10)
    public String Telephone;
    
    @Email
    public String Email;

    private String Password;

    @ManyToMany(cascade = CascadeType.ALL, fetch = FetchType.EAGER)
    @JoinTable(name = "people_roles",
                joinColumns = @JoinColumn(name = "people_id", referencedColumnName = "Id_People"),
                inverseJoinColumns = @JoinColumn(name ="rol_id", referencedColumnName = "Id_Role"))
    private Set<Role> roles = new HashSet<>();

    public People() {
    }

    public People(@NotEmpty(message = "Documento invalido o vacío") @Size(min = 8, max = 11) String document,
            @NotEmpty(message = "Nombre invalido o vacío") @Size(min = 5, max = 20) String name,
            @NotEmpty(message = "Dirección invalida o vacío") @Size(min = 20) String adress,
            @NotEmpty(message = "Teléfono invalido o vacío") @Size(min = 11) String telephone,
            @javax.validation.constraints.Email String email, String password) {
        Document = document;
        Name = name;
        Adress = adress;
        Telephone = telephone;
        Email = email;
        Password = password;
    }

    public People(Long id_People,
            @NotEmpty(message = "Documento invalido o vacío") @Size(min = 8, max = 11) String document,
            @NotEmpty(message = "Nombre invalido o vacío") @Size(min = 5, max = 20) String name,
            @NotEmpty(message = "Dirección invalida o vacío") @Size(min = 20) String adress,
            @NotEmpty(message = "Teléfono invalido o vacío") @Size(min = 11) String telephone,
            @javax.validation.constraints.Email String email, String password) {
        Id_People = id_People;
        Document = document;
        Name = name;
        Adress = adress;
        Telephone = telephone;
        Email = email;
        Password = password;
    }

    public Long getId_People() {
        return Id_People;
    }

    public void setId_People(Long id_People) {
        Id_People = id_People;
    }

    public String getDocument() {
        return Document;
    }

    public void setDocument(String document) {
        Document = document;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getAdress() {
        return Adress;
    }

    public void setAdress(String adress) {
        Adress = adress;
    }

    public String getTelephone() {
        return Telephone;
    }

    public void setTelephone(String telephone) {
        Telephone = telephone;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public Collection<Role> getRoles() {
        return roles;
    }

    public void setRoles(Set<Role> roles) {
        this.roles = roles;
    }

    public Object getId() {
        return null;
    }
}
