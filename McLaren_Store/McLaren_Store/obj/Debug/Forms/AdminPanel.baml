   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5       McLaren_Store$   McLaren_Store.Forms.AdminPanel   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5        � Title$  Admin Panel��5      $	��800��5       � WindowStartupLocation$ CenterScreen=�5      $	��450��5	       � WindowStyle$
 None=�5
       � AllowsTransparency . 
������  � 	Resources 5         PrimaryColor  SecondaryColor  PrimaryBrush �� Color  SecondaryBrush �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� CornerRadius��%]  &	        &	      &	 >     7   &	 V     7  '�� n     7  7  ��5   
   #212121��6.   ��5   
   #F5F5F5��60   �� 5   
   8   6/   �� 5   
   8   61   �� 5   
   " ��6   .�5      ��  � ��8   6+   �� 5       � ��8  6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� #��z��6!   $
 15��5    !   #��z��5!   !   #��z��5"   !   ._�5#      �� $��Center=�6/   $��Center=�6L   5$      5%      5'      5(      5)      .��5,      �� #��[ 6   $
 20��68    �� Padding$ 10q�6J   ._�5-   
   ]� ��. .+�5/      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�62   #��[ 6G   .+�50      �� -   5        $#��Добавить машину��50      $	��150��63    � Triggers 51      :� 52       :� RoutedEvent$ Button.PreviewMouseDown��6'   .[�53      �� .��54   "   �� .��55   &   W�  �� TargetProperty$- 'RenderTransform.(ScaleTransform.ScaleX)�66   	 W� To$	 0.9X�6z    g� Duration$ 0:0:0.1D�6�   W� 56   &   $- 'RenderTransform.(ScaleTransform.ScaleY)�66   $	 0.9X�6z   $ 0:0:0.1D�6�   57   #   58      59      :� 5:      $ Button.PreviewMouseUp��6'   .[�5;      �� .��5<   "   �� .��5=   &   W� $- 'RenderTransform.(ScaleTransform.ScaleX)�66   $ 1X�6z   $ 0:0:0.1D�6�   W� 5>   &   $- 'RenderTransform.(ScaleTransform.ScaleY)�66   $ 1X�6z   $ 0:0:0.1D�6�   5?   #   5@      5A      5C      �� 5D      -   5        $��
Отчёт��5D      $	��150��6)   65   �� 5E      -   5        $��База Данных��5E      $	��150��6/   6;   �� 5H      $ 
Horizontal=�6   $��
195 ,0,0,0q�66   $	��Top=�6K   $��42��6c   .+�5I      �� -   5        $	��—��5I      $��40��6)   64   �� 5J      -   5        $	��✖��5J      $��40��6)   64   5K      5L      �� 5O      $
��Auto=�6   $��10q�6>   .��5P      �� $ Vertical=�6   $��10q�64   .+�5Q      �� $5��/Статистика продаж по дням��6    #��[ 6A   $��16(�6n   $��0,10,0,5q�6|   #   LiveCharts.Wpf.CartesianChart  5R      $	��310��6H   &    LiveCharts.Wpf.Charts.Base.Chart   Series 6)   �� *SalesSeries+   AxisX 5T         LiveCharts.Wpf.Axis     Title$ Дата��6'      Labels 64   �� *Dates+   AxisY 5W        $' !Количество продаж��6'      LabelFormatter 6A   �� *	Formatter+5Y      5Z      5[      5\      5]      5^      