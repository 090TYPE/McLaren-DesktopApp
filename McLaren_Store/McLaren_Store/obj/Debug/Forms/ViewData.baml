   M S B A M L   `   `   `  ����  McLaren_Store"    McLaren_Store.Forms.ViewData   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$C  =Просмотр и редактирование данных��5   	   $	��600��62   $
��1000��6?    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0 0 0 10q�6.   .+�5      �� $&�� Выберите таблицу:��6   $��Center=�60   �� 5      -   5         ��Name$ TableSelector��5      $	��200��6.   $
��10 0q�6:   6H   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGridView��5      $��1��6)      AutoGenerateColumns .    CanUserAddRows .    CanUserDeleteRows . 	   
IsReadOnly	 .  $��0 0 0 10q�5   &   �� 5   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5      �� -   5        $��Обновить��5      $��0 0 10 0q�6>   �� 5      -   5        $+��%Сохранить изменения��5      63   5      5      5      