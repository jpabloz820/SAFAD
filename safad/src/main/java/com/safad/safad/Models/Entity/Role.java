package com.safad.safad.Models.Entity;

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToMany;
import javax.persistence.Table;

@Entity
@Table(name = "role")

public class Role
{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long Id_Role;

    private String Name_Role;
    
    @ManyToMany(mappedBy = "roles")
    private Set<People> people = new HashSet<>();

    public Role() {
    }

    public Role(Long id_Role, String name_Role, Set<People> people) {
        Id_Role = id_Role;
        Name_Role = name_Role;
        this.people = people;
    }

    public Role(String name_Role, Set<People> people) {
        Name_Role = name_Role;
        this.people = people;
    }

    public Long getId_Role() {
        return Id_Role;
    }

    public void setId_Role(Long id_Role) {
        Id_Role = id_Role;
    }

    public String getName_Role() {
        return Name_Role;
    }

    public void setName_Role(String name_Role) {
        Name_Role = name_Role;
    }

    public Collection<People> getPeople() {
        return people;
    }

    public void setPeople(Set<People> people) {
        this.people = people;
    }
}

