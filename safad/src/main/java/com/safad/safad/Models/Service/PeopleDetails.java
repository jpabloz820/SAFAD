package com.safad.safad.Models.Service;

import java.util.ArrayList;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.safad.safad.Models.Entity.People;
import com.safad.safad.Models.Entity.Role;
import com.safad.safad.Models.Repository.PeopleRepository;
import com.safad.safad.Models.Repository.RoleRepository;

@Service

public class PeopleDetails
{
    private PeopleRepository peopleRepository;
    private RoleRepository roleRepository;

    @Autowired

    public PeopleDetails(PeopleRepository peopleRepository, RoleRepository roleRepository)
    {
        this.peopleRepository = peopleRepository;
        this.roleRepository = roleRepository;
    }

    public boolean Login(String Email, String Password)
    {
        People people = peopleRepository.findByEmail(Email);

        if(people != null && people.getPassword().equals(Password))
        {
            return true;
        }

        return false;
    }

    public List<Role> GetRoles(String Email)
    {
        People people = peopleRepository.findByEmail(Email);
        
        return new ArrayList<>(people.getRoles());
    }
}