package com.safad.safad.Models.Repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import com.safad.safad.Models.Entity.Role;

public interface RoleRepository extends JpaRepository<Role, Long>
{
    @Query("SELECT u FROM Role u WHERE u.Id_Role = ?1")
    Role findById_Role(Long Id_Role);

    @Query("SELECT u FROM Role u WHERE u.Name_Role = ?1")
    Role findByName_Role(String Name_Role);
}
