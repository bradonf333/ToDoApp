import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAccountComponent } from './create-account/create-account.component';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './Services/auth-guard.service';
import { TodoAddComponent } from './todo-add/todo-add.component';
import { TodoDeleteComponent } from './todo-delete/todo-delete.component';
import { TodoListComponent } from './todo-list/todo-list.component';

const routes: Routes = [
  // { path: 'todo', component: TodoListComponent },
  // { path: 'todo-new', component: TodoAddComponent },
  { path: 'todo', component: TodoListComponent, canActivate: [AuthGuardService] },
  { path: 'todo-new', component: TodoAddComponent, canActivate: [AuthGuardService] },
  { path: 'todo-delete', component: TodoDeleteComponent },
  { path: 'login', component: LoginComponent },
  { path: 'create-account', component: CreateAccountComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
  // { path: 'login', component: LoginComponent },
  // { path: 'logout', component: LogoutComponent },
  // { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  // { path: 'users', component: UsersComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
