SELECT DISTINCT
    CONVERT(VARCHAR, HUI.DateofUpdate, 107) AS DateofUpdate,
    CONVERT(VARCHAR, HUI.DateofUpdate, 120) AS update_date,
    HUI.auto_id,
    CONCAT(DATEPART(HOUR, HUI.DateofUpdate), ':', DATEPART(MINUTE, HUI.DateofUpdate)) AS times 
FROM
    history_update_info HUI
GROUP BY
    HUI.DateofUpdate,
    HUI.auto_id
ORDER BY
		DateofUpdate DESC;
	

		select U.employee_name , U.employee_id,CONVERT(VARCHAR, HUI.dateofupdate,120) as update_date ,
		CONVERT(VARCHAR, HUI.DateofUpdate) AS DateofUpdate,
		CONCAT(DATEPART (HOUR, DateofUpdate) ,':',DATEPART (MINUTE, DateofUpdate)) as times ,
		CONCAT(HUI.FieldName, ': ', HUI.FieldValue) as descc 
		
		from history_update_info HUI 
		INNER JOIN users U on HUI.Updated_By=U.employee_id
		where HUI.auto_id='REQ_202306021536001' and HUI.DateofUpdate='2023-06-02 15:44:00' group by DateofUpdate,U.employee_id, U.employee_name, HUI.FieldName,HUI.FieldValue order by DateofUpdate desc


		select CONVERT(VARCHAR, HUI.DateofUpdate, 107) as DateofUpdate ,CONVERT(VARCHAR, HUI.DateofUpdate, 108) as times, u.employee_name,ms.status_name  From history_update_info hui
		inner join master_status ms on hui.FieldValue=ms.id
		inner join users u on hui.Updated_By=u.employee_id
		
		where auto_id='REQ_202306021536001' and fieldname='request_status'
		order by hui.DateofUpdate desc


		
SELECT DISTINCT CONVERT(date, dateofUpdate) AS DateWithoutTime
FROM history_update_info order by CONVERT(date, dateofUpdate) desc

select * from history_update_info where dateofUpdate>='2023-06-05 00:00:00' and dateofUpdate<='2023-06-05 23:59:00'