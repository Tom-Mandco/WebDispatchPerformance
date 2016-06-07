select  x.return_day, x.dayofweek, x.start_hour, x.returned_by, 
        count(x.web_order_number) number_of_orders, 
        sum(x.qty_returned) qty_returned,
        sum(x.qty_fit) qty_fit,
        sum(x.qty_Gala) qty_Gala,
        sum(x.qty_writeoff) qty_writeoff,
        sum(x.qty_attentn) qty_attentn
from
  
(select c.web_order_number, r.returned_by, r.returned_datetime,
        to_char(r.returned_datetime,'DD_MON_YYYY') return_day, 
        to_char(r.returned_datetime,'HH24') start_hour, 
        to_char(r.returned_datetime,'Day') dayofweek, 
        sum(c.qty_returned) qty_returned,
        sum(case when c.quality = 'F' then c.qty_returned else 0 end) qty_fit,
        sum(case when c.quality = 'G' then c.qty_returned else 0 end) qty_Gala,
        sum(case when c.quality = 'W' then c.qty_returned else 0 end) qty_writeoff,
        sum(case when c.quality = 'A' then c.qty_returned else 0 end) qty_attentn
  from mackays.web_cust_ret_hdr r,
       mackays.web_cust_returns c
 where trunc(r.returned_datetime) >= trunc(sysdate-7)
   and r.return_batch_id = c.return_batch_id
 group by c.web_order_number, r.returned_by, r.returned_datetime,
        to_char(r.returned_datetime,'DD_MON_YYYY'), 
        to_char(r.returned_datetime,'HH24'), 
        to_char(r.returned_datetime,'Day')) x
    
group by x.returned_by, x.return_day, x.start_hour, x.dayofweek
order by x.return_day desc, x.returned_by, x.start_hour