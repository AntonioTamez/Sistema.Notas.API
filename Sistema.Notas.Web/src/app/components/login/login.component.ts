import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports:[CommonModule, ReactiveFormsModule, RouterModule],
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  token: string = "";

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.loginForm.valid) { 

      const username = this.loginForm.value.username;
      const password = this.loginForm.value.password; 

      this.authService.getToken(username, password).subscribe(
        token => {          
          this.token = token;
          localStorage.setItem('token', token); 

          const decodedToken = this.decodeToken(token);

          const username = decodedToken.username; 
          const userRole = decodedToken.role; 
          const name = decodedToken.name; 

          console.log("el rol es: ", userRole);
          console.log("el name es: ", name);
          console.log("el username es: ", username);
           
          this.router.navigate(['/dashboard']);
        },
        error => {
          console.error('Error al obtener el token', error);
        }
      );
    }

    
  }

  trimInput(controlName: string) {
    const control = this.loginForm.get(controlName);
    if (control) {
      control.setValue(control.value.trim());
    }
  }

  private decodeToken(token: string): any {
    const payload = token.split('.')[1];
    const decodedPayload = atob(payload); 
    return JSON.parse(decodedPayload);
  }

}
