   M S B A M L   `   `   `  ����  McLaren_Store"    McLaren_Store.Forms.ViewData   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 	View Data��5   	   $	��600��6   $
��1000��6(    � 	Resources 5	      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value��%�  '        �� 5
   
   "  6   .�5      ��  � ��$ 14(�6)   �� 5       �@ 	RowHeight$ 35Q�6*   �� 5       �@ ColumnHeaderHeight$ 40Q�63   �� 5       � ��
 �����6+   �� 5        �@ AlternatingRowBackground
 �����69   �� 5       � ��
 �����6,   �� 5        �@ HorizontalGridLinesBrush
 �����69   �� 5       �@ VerticalGridLinesBrush
 �����67   5      5      .��5      � $��10q�6   .+�5   
   �� .@�5      �� $
��Cars��6   .��5        -   5          Name$ CarsDataGrid��5         AutoGenerateColumns .    
IsReadOnly .  	   CanUserAddRows	 . 
   CanUserDeleteRows
 .    AlternatingRowBackground
 �����   RowBackground
 �����$��0,0,0,50q�5       5"      5#      �� 5&      $��	Customers��6   .��5'        -   5        $ CustomersDataGrid��5'       .  .  	 . 
 . 
 �����
 �����$��0,0,0,50q�5.      50      51      �� 54      $��Sales��6   .��55        -   5        $ SalesDataGrid��55       .  .  	 . 
 . 
 �����
 �����$��0,0,0,50q�5<      5>      5?      �� 5B      $��	Employees��6   .��5C        -   5        $ EmployeesDataGrid��5C       .  .  	 . 
 . 
 �����
 �����$��0,0,0,50q�5J      5L      5M      �� 5P      $��Roles��6   .��5Q        -   5        $ RolesDataGrid��5Q       .  .  	 . 
 . 
 �����
 �����$��0,0,0,50q�5X      5Z      5[      5\      �� 5_   
   -   5        $��Save Changes��5_      $��Right=�5`      $��Bottom=�5a      $	��120��5b      $��
0,10,10,10q�5c      5d      5e      5f      