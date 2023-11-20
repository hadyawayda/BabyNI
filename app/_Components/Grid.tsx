'use client'

import { Grid, GridColumn as Column } from '@progress/kendo-react-grid'

interface Data {
   datetimE_KEY: number
   time: Date
   networK_SID: number
   nealias: string
   netype: string
   rsL_INPUT_POWER: number
   maX_RX_LEVEL: number
   rsL_DEVIATION: number
}

interface GridProps {
   props: Data[]
}

const GridComponent = ({ props }: GridProps) => {
   return (
      <div className="w-10/12 text-black">
         <div className="my-6">Performance Grid</div>
         <div>
            <Grid
               style={{
                  width: '100%',
                  height: '500px',
                  border: '1px solid #999',
               }}
               data={props}
               // filterable={true}
               pageable={{
                  buttonCount: 10,
                  previousNext: true,
               }}
               pageSize={24}
               reorderable={true}
               resizable={true}
               sortable={true}
            >
               <Column
                  field="DATETIME_KEY"
                  title="DATETIME_KEY"
                  width="130px"
               />
               <Column
                  field="TIME"
                  title="TIME"
                  width="170px"
                  locked={true}
               />
               <Column
                  field="NETWORK_SID"
                  title="NETWORK_SID"
                  width="140px"
               />
               <Column
                  field="NEALIAS"
                  title="NEALIAS"
                  width="140px"
               />
               <Column
                  field="NETYPE"
                  title="NETYPE"
                  width="140px"
               />
               <Column
                  field="RSL_INPUT_POWER"
                  title="RSL_INPUT_POWER"
                  width="170px"
               />
               <Column
                  field="MAX_RX_LEVEL"
                  title="MAX_RX_LEVEL"
                  width="140px"
               />
               <Column
                  field="RSL_DEVIATION"
                  title="RSL_DEVIATION"
                  width="140px"
               />
            </Grid>
         </div>
      </div>
   )
}

export default GridComponent
