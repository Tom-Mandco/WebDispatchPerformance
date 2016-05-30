
select x.despatch_day, x.dayofweek,
       x.despatch_by, x.despatch_min start_time,
       sum(case when x.package_number = 1 then 1 else 0 end) ords,
       sum(x.quantity_despatched) qty
  from 
(   
select to_char(p.despatch_date,'DD_MON_YYYY') despatch_day, 
       to_char(p.despatch_date,'HH24') ||':'|| 
       substr(to_char(p.despatch_date,'MI'),1,1) ||'0' despatch_min,
       to_char(p.despatch_date,'Day') dayofweek, 
       p.despatch_by,
       p.package_number, p.channel_number, p.order_number,  
       (select count(i.package_number) 
          from mackays.pos_order_package_item i
         where p.channel_number = i.channel_number
           and p.order_number = i.order_number
           and p.package_number = i.package_number) number_of_items,
       (select sum(i.qty_despatched)
          from mackays.pos_order_package_item i
         where p.channel_number = i.channel_number
           and p.order_number = i.order_number
           and p.package_number = i.package_number) quantity_despatched
  from mackays.pos_order_package p
 where trunc(p.despatch_date) >= trunc(sysdate-7)
   and p.despatch_by <> 'ep2050'
 ) x
  group by x.despatch_day, x.despatch_min, x.despatch_by, x.dayofweek
  order by x.despatch_day desc, x.despatch_min, x.despatch_by
