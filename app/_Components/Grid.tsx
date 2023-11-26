'use client'

import { GridComponentProps as gridProps } from './Interfaces/Interfaces'
import { Grid as G, GridColumn as Column } from '@progress/kendo-react-grid'

const Grid = ({ props }: gridProps) => {
   return (
      <div className="w-10/12 text-black">
         <div className="my-6 w-full flex justify-center">Performance Grid</div>
         <div>
            <G
               style={{
                  width: '100%',
                  height: '300px',
                  border: '1px solid #999',
               }}
               data={props}
               // filterable={true}
               pageable={{
                  buttonCount: 10,
                  previousNext: true,
               }}
               pageSize={10}
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
            </G>
         </div>
      </div>
   )
}

export default Grid
