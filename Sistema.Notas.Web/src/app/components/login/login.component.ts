import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CursosService } from '../../services/cursos.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports:[CommonModule, ReactiveFormsModule],
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  token: string = "";

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log('Form Submitted', this.loginForm.value);

      const username = this.loginForm.value.username;
      const password = this.loginForm.value.password;

      
      console.log('Form Submitted', username, password);

      this.authService.getToken(username, password).subscribe(
        token => {          
          this.token = token; // Almacena el token en la variable
          localStorage.setItem('token', token); // Opcional: Almacena el token en el localStorage
          console.log('Token:', token); // Opcional: Imprime el token en la consola
          // Redirigir o realizar otras acciones después de obtener el token
        },
        error => {
          console.error('Error al obtener el token', error);
        }
      );
    }

    
  }
}
