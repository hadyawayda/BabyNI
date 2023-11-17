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
   //  console.log(props)

   return (
      <div className="w-96 ml-12 text-black">
         <div className="mb-8">Performance Grid</div>
         <div>
            <Grid
               style={{
                  width: '600px',
                  border: '1px solid #000',
                  backgroundColor: 'darkgray',
                  overflow: 'scroll',
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
                  field="networK_SID"
                  title="NETWORK_SID"
                  width="140px"
                  locked={true}
               />
               <Column
                  field="time"
                  title="TIME"
                  width="250px"
               />
               <Column
                  field="datetimE_KEY"
                  title="DATETIME_KEY"
                  width="140px"
               />
               <Column
                  field="nealias"
                  title="NEALIAS"
                  width="140px"
               />
               <Column
                  field="netype"
                  title="NETYPE"
                  width="140px"
               />
               <Column
                  field="rsL_INPUT_POWER"
                  title="RSL_INPUT_POWER"
                  width="140px"
               />
               <Column
                  field="maX_RX_LEVEL"
                  title="MAX_RX_LEVEL"
                  width="140px"
               />
               <Column
                  field="rsL_DEVIATION"
                  title="RSL_DEVIATION"
                  width="140px"
               />
            </Grid>
         </div>
      </div>
   )
}

export default GridComponent
