package com.safad.safad.Models.Repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import com.safad.safad.Models.Entity.People;

public interface PeopleRepository extends JpaRepository<People, Long>
{
    @Query("SELECT u FROM People u WHERE u.Email = ?1")
    People findByEmail(String Email);

    @Query("SELECT u FROM People u WHERE u.Id_People = ?1")
    People findById_People(Long Id_People);
}

