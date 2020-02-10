import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpClientModule, HttpHeaders } from "@angular/common/http"

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder, private http:HttpClient) { }
  readonly rootURL="https://localhost:44332/api";

  formModel=this.fb.group({
    UserName:['',Validators.required],
    Email:['', Validators.email],
    FullName:[''],
    Passwords:this.fb.group({
      Password:['', [Validators.required,Validators.minLength(4)]],
      ConfirmPassword:['',Validators.required]
    },{validator:this.comparePassword})
  });

  comparePassword(fb:FormGroup){
    let confirmPasswordCtrl=fb.get('ConfirmPassword');
    if(confirmPasswordCtrl.errors==null || 'passwordMismatch' in confirmPasswordCtrl.errors){
      if(fb.get('Password').value!=confirmPasswordCtrl.value){
        confirmPasswordCtrl.setErrors({passwordMismatch:true})
      }
      else{
        confirmPasswordCtrl.setErrors(null)
      }
    }
  }

  register(){
    var body={
      UserName:this.formModel.value.UserName,
      FullName:this.formModel.value.FullName,
      Email:this.formModel.value.email,
      Password: this.formModel.value.Passwords.Password,
    }
    return this.http.post(this.rootURL+'/ApplicationUser/Register',body);
  }

  login(formData){
    return this.http.post(this.rootURL+'/ApplicationUser/Login',formData);
  }

  getUserProfile(){
    return this.http.get(this.rootURL+'/UserProfile');
  }

  roleMatch(allowedRoles):boolean{
    var isMatch = false;
    var payload = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payload.role;
    allowedRoles.forEach(element => {
      if(userRole == element){
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
}
