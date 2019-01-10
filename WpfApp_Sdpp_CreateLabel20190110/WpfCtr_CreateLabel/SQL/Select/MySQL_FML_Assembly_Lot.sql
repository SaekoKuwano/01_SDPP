SELECT 
nm_proname as ProName
,nm_lotid as AssemblyLot
,qy_cnt as Cnt
,mk_class as KouSei
,max(dt_data) as DtPro
FROM
sdpp.sdpp_fml_proinfo
where
nm_lotid = '_LotId'