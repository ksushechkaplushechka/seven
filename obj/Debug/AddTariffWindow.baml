   M S B A M L   `   `   `  ����  Autobike    Autobike.AddTariffWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Добавить тариф��5   	   $	��250��6    $	��400��6-   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5   
   " ��6+   .�5	      ��  � �� 5
      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � $��10,35,10,10q�6   
 � RowDefinitions
 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5#   
   �� -   5        $��X��5#      $��30��6   $��30��6(   $��Right=�64   $	��Top=�6P   
������ ���#E74C3C
������ $
��Bold"�5$   Y   $��0,5,5,0q�5%   +   �� 5(   
   $$��Название тарифа:��6   $��0��6,   
�������$��10q�6L   $��Center=�6X   �� 5)   
   -   5         ��Name$ TariffNameTextBox��5)      $��0��6-   $��120,10,101,10q�6:   ���#3A3A3A
�������
�������$��1q�5*   M   $��30��6a   �� 5-   
   $��	Цена:��6   $��1��6!   
�������$��10q�6A   $��Center=�6M   �� 5.   
   -   5        $ TariffPriceTextBox��5.      $��1��6.   $��120,10,101,10q�6;   ���#3A3A3A
�������
�������$��1q�5/   M   $��30��6a   �� 52   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,20,0,0q�6X   .+�53      �� -   5        $��Сохранить��53      $	��100��6)   $��10q�65   ���#4CAF50
����������#4CAF50$��1q�54   S   #��[  55      6A   56      57      58      